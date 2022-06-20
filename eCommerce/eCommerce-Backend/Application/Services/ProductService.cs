using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.Common;
using Microsoft.EntityFrameworkCore;
using eCommerce_SharedViewModels.Enums;
using static eCommerce_SharedViewModels.Utilities.Constants.SystemConstants;
using eCommerce_Backend.Application.Common;
using System.Net.Http.Headers;
using eCommerce_SharedViewModels.Utilities.Constants;

namespace eCommerce_Backend.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly eCommerceDbContext _dbContext;
        private readonly IFileStorage _fileStorage;
        public ProductService(eCommerceDbContext dbContext,
            IFileStorage fileStorage)
        {
            _dbContext = dbContext;
            _fileStorage = fileStorage;
        }

        public async Task<ApiResult<bool>> Create(ProductCreateDto request)
        {
            var Product = new Products()
            {
                ProductName = request.ProductName,
                CategoiesId = request.CategoryId,
                Description = request.Description,
                Price = request.Price,
                CreatedDate = DateTime.Now.Date,
                UpdatedDate = DateTime.Now.Date,
                Status = Status.Available,
            };
            // Save image
            if (request.ThumbnailImage != null)
            {
                Product.ProductImages = new List<ProductImages>()
                {
                    new ProductImages()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                    }
                };
            }

            using (_dbContext)
            {
                _dbContext.Add(Product);
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<ProductReadDto>> GetById(int Id)
        {
            var data = await _dbContext.Products.FindAsync(Id);
            var image = await _dbContext.ProductImages.Where(x => x.ProductsId == Id && x.IsDefault == true).FirstOrDefaultAsync();
            if (data == null)
                return new ApiErrorResult<ProductReadDto>(ErrorMessage.ProductNotFound);
            var result = new ProductReadDto()
            {
                ProductName=data.ProductName,
                CreatedDate=DateTime.Now.Date,
                UpdatedDate=DateTime.Now.Date,
                Description = data.Description,
                Price=data.Price,
                Status =data.Status,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg"
            };
            return new ApiSuccessResult<ProductReadDto>(result);
        }

        public async Task<List<ProductReadDto>> GetList()
        {
            using (_dbContext)
            {
                var data = await _dbContext.Products.Where(x=>x.Status == Status.Available).Select(x => new ProductReadDto()
                {
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    Price = x.Price,
                    ProductName = x.ProductName,
                    UpdatedDate = x.UpdatedDate,
                }).ToListAsync();
                return data;
            }              
        }

        public async Task<PagedResult<ProductReadDto>> GetPaging(ProductPagingDto request)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.Products
                            join pi in _dbContext.ProductImages on p.Id equals pi.ProductsId into ppi
                            from pi in ppi.DefaultIfEmpty()
                            where pi == null || pi.IsDefault == true && p.Status == Status.Available
                            select new { p, pi };
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.p.ProductName.Contains(request.Keyword));
                }
                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ProductReadDto()
                    {
                        ProductName = x.p.ProductName,
                        CreatedDate = x.p.CreatedDate,
                        Description = x.p.Description,
                        Price = x.p.Price,
                        UpdatedDate = x.p.UpdatedDate,
                        ThumbnailImage = x.pi.ImagePath
                    }).ToListAsync();
                var pagedResult = new PagedResult<ProductReadDto>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedResult;
            }
        }

        public async Task<ApiResult<bool>> SoftDelete(int Id)
        {
            using (_dbContext)
            {
                var data = await _dbContext.Products.FindAsync(Id);
                if (data == null)
                {
                    return new ApiErrorResult<bool>(ErrorMessage.ProductNotFound);
                }
                data.Status = Status.Disable;
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        
        }

        public async Task<ApiResult<bool>> Update(int Id, ProductUpdateDto request)
        {
            using (_dbContext)
            {
                var data = await _dbContext.Products.FindAsync(Id);
                if (data == null)
                    return new ApiErrorResult<bool>(ErrorMessage.ProductNotFound);
                if (await _dbContext.Products.AnyAsync(x => x.ProductName == request.ProductName && x.Id != Id))
                {
                    return new ApiErrorResult<bool>(ErrorMessage.ProductNameExists);
                }
                data.UpdatedDate = DateTime.Now.Date;
                data.ProductName = request.ProductName;
                data.Price = request.Price;
                data.Description = request.Description;
                data.CategoiesId = request.CategoiesId;
                //Save image
                if (request.ThumbnailImage != null)
                {
                    var thumbnailImage = await _dbContext.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductsId == Id);
                    if (thumbnailImage != null)
                    {
                        thumbnailImage.FileSize = request.ThumbnailImage.Length;
                        thumbnailImage.ImagePath = await SaveFile(request.ThumbnailImage);
                        _dbContext.ProductImages.Update(thumbnailImage);
                    }
                }
                _dbContext.Products.Update(data);
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorage.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + RESOURCES +"/"+ USER_IMAGES_FOLDER_NAME + "/" + fileName;
        }
    }
}

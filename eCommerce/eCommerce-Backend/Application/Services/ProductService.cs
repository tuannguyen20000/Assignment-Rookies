using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.Common;
using Microsoft.EntityFrameworkCore;
using eCommerce_SharedViewModels.Enums;
using static eCommerce_SharedViewModels.Utilities.Constants.SystemConstants;

namespace eCommerce_Backend.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly eCommerceDbContext _dbContext;
        public ProductService(eCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<bool>> Create(ProductCreateDto request)
        {
            var Product = new Products()
            {
                ProductName = request.ProductName,
                CategoiesId = request.CategoryId,
                Description = request.Description,
                ImagessURL = request.ImagessURL,
                Price = request.Price,
                CreatedDate = DateTime.Now.Date,
                UpdatedDate = DateTime.Now.Date,
            };

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
            if (data == null)
                return new ApiErrorResult<ProductReadDto>(ErrorMessage.ProductNotFound);
            var result = new ProductReadDto()
            {
                ProductName=data.ProductName,
                CreatedDate=DateTime.Now.Date,
                UpdatedDate=DateTime.Now.Date,
                Description = data.Description,
                ImagessURL  =data.ImagessURL,
                Price=data.Price,
                Status =data.Status
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
                    ImagessURL = x.ImagessURL,
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
                var query = _dbContext.Products.Where(x => x.Status == Status.Available).AsQueryable();
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.ProductName.Contains(request.Keyword));
                }
                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ProductReadDto()
                    {
                        ProductName = x.ProductName,
                        CreatedDate = x.CreatedDate,
                        Description = x.Description,
                        Price = x.Price,
                        ImagessURL = x.ImagessURL,
                        UpdatedDate = x.UpdatedDate
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
                data.ImagessURL = request.ImagessURL;
                data.Description = request.Description;
                data.CategoiesId = request.CategoiesId;
                _dbContext.Products.Update(data);
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }
    }
}

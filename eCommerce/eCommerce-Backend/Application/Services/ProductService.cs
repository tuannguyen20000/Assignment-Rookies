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
using eCommerce_SharedViewModels.Exceptions;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductImage;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;

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

        public async Task<ApiResult<bool>> AddComment(int Id, ProductRatingCreateDto request)
        {
            var data = await _dbContext.Products.FindAsync(Id);
            if (data == null)
            {
                return new ApiErrorResult<bool>(ErrorMessage.ProductNotFound);
            }
            var product = new ProductRatings()
            {
                Comment = request.Comment,
                Rating = request.Rating,
                TimeStamp = DateTime.Now,
                Title = request.Title,
                UserEmail = request.UserEmail,
                UserName = request.UserName,
                ProductsId = Id
            };
            using (_dbContext)
            {
                _dbContext.ProductRatings.Add(product);
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<bool>> AddImage(int Id, ProductImageCreateDto request)
        {
            var Image = new ProductImages()
            {
                ProductsId = Id,
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
            };

            if (request.ImageFile != null)
            {
                Image.ImagePath = await SaveFile(request.ImageFile);
                Image.FileSize = request.ImageFile.Length;
            }
            using (_dbContext)
            {
                _dbContext.ProductImages.Add(Image);
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }

        }

        public async Task<ApiResult<bool>> CategoryAssign(int Id, CategoryAssignDto request)
        {
            using (_dbContext)
            {
                var data = await _dbContext.Products.FindAsync(Id);
                if (data == null)
                {
                    return new ApiErrorResult<bool>(ErrorMessage.ProductNotFound);
                }
                foreach (var category in request.Categories)
                {
                    var productInCategory = await _dbContext.ProductInCategory
                        .FirstOrDefaultAsync(x => x.CategoriesId == int.Parse(category.Id)
                        && x.ProductsId == Id);
                    if (productInCategory != null && category.Selected == false)
                    {
                        _dbContext.ProductInCategory.Remove(productInCategory);
                    }
                    else if (productInCategory == null && category.Selected)
                    {
                        await _dbContext.ProductInCategory.AddAsync(new ProductInCategory()
                        {
                            CategoriesId = int.Parse(category.Id),
                            ProductsId = Id
                        });
                    }
                }
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }       
        }

        public async Task<ApiResult<bool>> Create(ProductCreateDto request)
        {
            var Product = new Products()
            {
                ProductName = request.ProductName,
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
            if (data == null)
                return new ApiErrorResult<ProductReadDto>(ErrorMessage.ProductNotFound);
            var image = await _dbContext.ProductImages.Where(x => x.ProductsId == Id && x.IsDefault == true).FirstOrDefaultAsync();
            var listSubImage = await _dbContext.ProductImages.Where(x => x.ProductsId == Id && x.IsDefault == false).Select(x=> new ProductImageDto()
            {
                ImagePath = x.ImagePath,
                Caption = x.Caption,
                DateCreated = x.DateCreated,
                FileSize = x.FileSize,
                Id = x.Id,
                IsDefault = x.IsDefault,
                ProductsId = x.ProductsId
            }).ToListAsync();
            var categories = await (from c in _dbContext.Categories
                                    join pic in _dbContext.ProductInCategory on c.Id equals pic.CategoriesId
                                    where pic.ProductsId == Id && c.Status == Status.Available
                                    select c.CategoryName).ToListAsync();
            var ratings = await (from r in _dbContext.ProductRatings.OrderByDescending(x => x.Id)
                                 join p in _dbContext.Products on r.ProductsId equals p.Id into pr
                                 from p in pr.DefaultIfEmpty()
                                 where r.ProductsId == Id
                                 select r).ToListAsync();
            double avrRating = 0;
            if (ratings.Count != 0)
            {
                avrRating = ratings.Where(x => x.Rating.HasValue).Select(x => x.Rating.Value).Average();
            }
            var ListComment = ratings.Select(x => new ProductRatingDto
            {
                Comment = x.Comment,
                Id = x.Id,
                ProductsId = x.ProductsId,
                Rating = x.Rating,
                TimeStamp = x.TimeStamp,
                Title = x.Title,
                UserEmail = x.UserEmail,
                UserName = x.UserName,
            }).ToList();
            var result = new ProductReadDto()
            {
                Id = data.Id,
                ProductName=data.ProductName,
                CreatedDate=DateTime.Now.Date,
                UpdatedDate=DateTime.Now.Date,
                Description = data.Description,
                Price=data.Price,
                Status =data.Status,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg",
                Categories = categories,
                avrRating = (int?)Math.Ceiling(avrRating),
                Comments = ListComment,
                SubImages = listSubImage
            };
            return new ApiSuccessResult<ProductReadDto>(result);
        }

        public async Task<ApiResult<AvgRatingDto>> GetAvgRatingById(int Id)
        {
            var product = await _dbContext.Products.FindAsync(Id);
            if (product == null)
                return new ApiErrorResult<AvgRatingDto>(ErrorMessage.ProductNotFound);
            var ratings = await (from r in _dbContext.ProductRatings.OrderByDescending(x => x.Id)
                                 join p in _dbContext.Products on r.ProductsId equals p.Id into pr
                                 from p in pr.DefaultIfEmpty()
                                 where r.ProductsId == Id
                                 select r).ToListAsync();
            double avrRating = 0;
            if (ratings.Count != 0)
            {
                avrRating = ratings.Where(x => x.Rating.HasValue).Select(x => x.Rating.Value).Average();
            }
            var countComment = ratings.Where(x => x.ProductsId == Id && x.Rating.HasValue).Select(x => x.Comment).Count();
            var data = new AvgRatingDto()
            {
                avgRating = (int)Math.Ceiling(avrRating),
                countComment = countComment
            };
            return new ApiSuccessResult<AvgRatingDto>(data);

        }

        public async Task<ApiResult<ProductImageDto>> GetImageById(int imageId)
        {
            var image = await _dbContext.ProductImages.FindAsync(imageId);
            if (image == null)
            {
                throw new eComExceptions($"Cannot find an image with id {imageId}");
            }

            var dto = new ProductImageDto()
            {
                ProductsId = image.ProductsId,
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
            };
            return new ApiSuccessResult<ProductImageDto>(dto);
        }

        public async Task<List<ProductReadDto>> GetList()
        {
            using (_dbContext)
            {

                var data = await _dbContext.Products
                    .Include(x=>x.ProductImages)
                    .Include(x => x.ProductInCategory)
                    .Where(x=>x.Status == Status.Available).Select(x => new ProductReadDto()
                {
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    Price = x.Price,
                    ProductName = x.ProductName,
                    UpdatedDate = x.UpdatedDate,
                    CategoryId = x.ProductInCategory.Select(x=>x.CategoriesId).FirstOrDefault(),
                    ThumbnailImage = x.ProductImages.Where(x=>x.IsDefault == true).Select(x=>x.ImagePath).FirstOrDefault()
                }).ToListAsync();
                return data;
            }              
        }

        public async Task<List<ProductImageDto>> GetListImageByProductId(int Id)
        {
            using (_dbContext)
            {
                return await _dbContext.ProductImages.Where(x => x.ProductsId == Id)
                .Select(i => new ProductImageDto()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    ProductsId = i.ProductsId,
                }).ToListAsync();
            }        
        }

        public async Task<PagedResult<ProductReadDto>> GetPaging(ProductPagingDto request)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.Products.OrderByDescending(x => x.Id).Distinct()
                            join pi in _dbContext.ProductImages on p.Id equals pi.ProductsId into ppi
                            from pi in ppi.DefaultIfEmpty()
                            join pic in _dbContext.ProductInCategory on p.Id equals pic.ProductsId into ppic
                            from pic in ppic.DefaultIfEmpty()
                            join c in _dbContext.Categories on pic.CategoriesId equals c.Id into picc
                            from c in picc.DefaultIfEmpty()
                            join r in _dbContext.ProductRatings on p.Id equals r.ProductsId into ric
                            from r in ric.DefaultIfEmpty()
                            where pi == null || pi.IsDefault == true && p.Status == Status.Available
                            select new { p, pi, pic,
                                CategoryName = p.ProductInCategory.Count != 0 && c.Status == Status.Available ? c.CategoryName : "Uncategory" ,
                                CategoryId = p.ProductInCategory.Count != 0 && c.Status == Status.Available ? c.Id: 0, 
                                avrRating =  p.ProductRatings.Count != 0 ? (int)p.ProductRatings.Select(x => x.Rating).Average() : 0,
                                countComment = p.ProductRatings.Count != 0 ? p.ProductRatings.Select(x => x.Comment).Count() : 0
                            };

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.p.ProductName.Contains(request.Keyword));
                }
                if (request.CategoriesId != null && request.CategoriesId != 0)
                {
                    query = query.Where(p => p.pic.CategoriesId == request.CategoriesId);
                }
                
                int totalRow = await query.Select(x=>x.p.Id).Distinct().CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ProductReadDto()
                    {
                        Id = x.p.Id,
                        ProductName = x.p.ProductName,
                        CreatedDate = x.p.CreatedDate,
                        Description = x.p.Description,
                        Price = x.p.Price,
                        UpdatedDate = x.p.UpdatedDate,
                        ThumbnailImage = x.pi.ImagePath,
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        avrRating = (int)Math.Ceiling((decimal)x.avrRating),
                        countComment = x.countComment
                    }).Distinct().ToListAsync();
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

        public async Task<ApiResult<bool>> RemoveImage(int imageId)
        {
            var image = await _dbContext.ProductImages.FindAsync(imageId);
            if (image == null)
            {
                throw new eComExceptions($"Cannot find an image with id {imageId}");
            }
            _dbContext.ProductImages.Remove(image);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
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

        public async Task<ApiResult<bool>> UpdateImage(int imageId, ProductImageUpdateDto request)
        {
            var image = await _dbContext.ProductImages.FindAsync(imageId);
            if (image == null)
            {
                throw new eComExceptions($"Cannot find an image with id {imageId}");
            }

            if (request.ImageFile != null)
            {
                image.ImagePath = await SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }
            using (_dbContext)
            {
                _dbContext.ProductImages.Update(image);
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

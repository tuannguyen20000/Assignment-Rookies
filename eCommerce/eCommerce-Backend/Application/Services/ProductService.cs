using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.EntitiesDto.ProductDto;
using eCommerce_SharedViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_Backend.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly eCommerceDbContext _dbContext;
        public ProductService(eCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<bool>> Create(ProductsCreateDto request)
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

        public async Task<List<ProductReadDto>> GetListProduct()
        {
            var data = await _dbContext.Products.Select(x => new ProductReadDto()
            {
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                ImagessURL = x.ImagessURL,
                Price = x.Price,
                ProductName = x.ProductName,
                UpdatedDate = x.UpdatedDate
            }).ToListAsync();
            return data;
        }

        public async Task<PagedResult<ProductReadDto>> GetPagingProduct(ProductPagingDto request)
        {
            var query = _dbContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.ProductName.Contains(request.Keyword));
            }
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductReadDto()
                {
                    ProductName=x.ProductName,
                    CreatedDate=x.CreatedDate,
                    Description=x.Description,
                    Price=x.Price,
                    ImagessURL=x.ImagessURL,
                    UpdatedDate=x.UpdatedDate
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
}

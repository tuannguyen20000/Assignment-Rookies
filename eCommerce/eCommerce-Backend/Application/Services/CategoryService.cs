using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using eCommerce_SharedViewModels.Common;
using static eCommerce_SharedViewModels.Utilities.Constants.SystemConstants;
using eCommerce_SharedViewModels.Enums;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_Backend.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly eCommerceDbContext _dbContext;
        public CategoryService(eCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResult<bool>> Create(CategoryCreateDto request)
        {
            var category = new Categories()
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
            };

            using (_dbContext)
            {
                _dbContext.Add(category);
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<CategoryReadDto>> GetById(int Id)
        {
            var data = await _dbContext.Categories.FindAsync(Id);
            if (data == null)
                return new ApiErrorResult<CategoryReadDto>(ErrorMessage.CategoryNotFound);
            var result = new CategoryReadDto()
            {
                CategoryName = data.CategoryName,
                Description = data.Description,
                Status = data.Status
                
            };
            return new ApiSuccessResult<CategoryReadDto>(result);
        }

        public async Task<List<CategoryReadDto>> GetList()
        {
            using (_dbContext)
            {
                var data = await _dbContext.Categories.Where(x => x.Status == Status.Available).Select(x => new CategoryReadDto()
                {
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                }).ToListAsync();
                return data;
            }
        }

        public async Task<PagedResult<CategoryReadDto>> GetPaging(CategoryPagingDto request)
        {
            using (_dbContext)
            {
                var query = _dbContext.Categories.Where(x => x.Status == Status.Available).AsQueryable();
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.CategoryName.Contains(request.Keyword));
                }
                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new CategoryReadDto()
                    {
                        CategoryName = x.CategoryName,
                        Description = x.Description,
                    }).ToListAsync();
                var pagedResult = new PagedResult<CategoryReadDto>()
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
                var data = await _dbContext.Categories.FindAsync(Id);
                if (data == null)
                {
                    return new ApiErrorResult<bool>(ErrorMessage.CategoryNotFound);
                }
                data.Status = Status.Disable;
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<bool>> Update(int Id, CategoryUpdateDto request)
        {
            using (_dbContext)
            {
                var data = await _dbContext.Categories.FindAsync(Id);
                if (data == null)
                    return new ApiErrorResult<bool>(ErrorMessage.ProductNotFound);
                if (await _dbContext.Categories.AnyAsync(x => x.CategoryName == request.CategoryName && x.Id != Id))
                {
                    return new ApiErrorResult<bool>(ErrorMessage.ProductNameExists);
                }
                data.CategoryName = request.CategoryName;
                data.Description = request.Description;
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }
    }
}

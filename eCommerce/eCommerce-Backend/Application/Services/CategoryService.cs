using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using eCommerce_SharedViewModels.Common;

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

        public Task<ApiResult<CategoryReadDto>> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryReadDto>> GetListProduct()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<CategoryReadDto>> GetPaging(CategoryPagingDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> SoftDelete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Update(int Id, CategoryUpdateDto request)
        {
            throw new NotImplementedException();
        }
    }
}

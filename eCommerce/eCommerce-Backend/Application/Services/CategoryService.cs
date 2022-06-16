using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.EntitiesDto.CategoriesDto;
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
        public async Task<ApiResult<bool>> Create(CategoriesCreateDto request)
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
    }
}

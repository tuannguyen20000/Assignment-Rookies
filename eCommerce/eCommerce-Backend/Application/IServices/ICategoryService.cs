using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using eCommerce_SharedViewModels.EntitiesDto.Product;

namespace eCommerce_Backend.Application.IServices
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryReadDto>> GetPagingAsync(CategoryPagingDto request);
        Task<List<CategoryReadDto>> GetListAsync();
        Task<ApiResult<bool>> CreateAsync(CategoryCreateDto request);
        Task<ApiResult<bool>> UpdateAsync(int Id, CategoryUpdateDto request);
        Task<ApiResult<CategoryReadDto>> GetByIdAsync(int Id);
        Task<ApiResult<bool>> SoftDeleteAsync(int Id);
        Task<List<ProductReadDto>> GetListProductByIdAsync(int categoryId);
    }
}

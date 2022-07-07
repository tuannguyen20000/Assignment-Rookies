using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Categories;

namespace eCommerce_CustomerSite.Api.IServices
{
    public interface ICategoryApi
    {
        Task<PagedResult<CategoryReadDto>> GetPagingCategoryAsync(CategoryPagingDto request);
        Task<ApiResult<CategoryReadDto>> GetByIdAsync(int Id);
        Task<List<CategoryReadDto>> GetListAsync();
    }
}

using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using Refit;

namespace eCommerce_CustomerSite.Api.Client
{
    public interface ICategoryClient
    {
        [Get("/api/Categories/get-paging-category")]
        Task<PagedResult<CategoryReadDto>> GetPagingCategoryAsync(CategoryPagingDto request);
        [Get("/api/Categories/get-by-id/{Id}")]
        Task<ApiResult<CategoryReadDto>> GetByIdAsync(int Id);
        [Get("/api/Categories/get-list-category")]
        Task<List<CategoryReadDto>> GetListAsync();
    }
}

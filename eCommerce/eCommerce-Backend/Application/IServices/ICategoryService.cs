using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Categories;

namespace eCommerce_Backend.Application.IServices
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryReadDto>> GetPaging(CategoryPagingDto request);
        Task<List<CategoryReadDto>> GetList();
        Task<ApiResult<bool>> Create(CategoryCreateDto request);
        Task<ApiResult<bool>> Update(int Id, CategoryUpdateDto request);
        Task<ApiResult<CategoryReadDto>> GetById(int Id);
        Task<ApiResult<bool>> SoftDelete(int Id);
    }
}

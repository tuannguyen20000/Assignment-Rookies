using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.CategoriesDto;

namespace eCommerce_Backend.Application.IServices
{
    public interface ICategoryService
    {
        Task<ApiResult<bool>> Create(CategoriesCreateDto request);
    }
}

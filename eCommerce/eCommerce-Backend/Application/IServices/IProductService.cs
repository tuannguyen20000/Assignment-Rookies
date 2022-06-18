using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.Common;

namespace eCommerce_Backend.Application.IServices
{
    public interface IProductService
    {
        Task<PagedResult<ProductReadDto>> GetPaging (ProductPagingDto request);
        Task<List<ProductReadDto>> GetList();
        Task<ApiResult<bool>> Create(ProductCreateDto request);
        Task<ApiResult<bool>> Update(int Id,ProductUpdateDto request);
        Task<ApiResult<ProductReadDto>> GetById(int Id);
        Task<ApiResult<bool>> SoftDelete(int Id);
    }
}

using eCommerce_SharedViewModels.EntitiesDto.ProductDto;
using eCommerce_SharedViewModels.Common;

namespace eCommerce_Backend.Application.IServices
{
    public interface IProductService
    {
        Task<ApiResult<bool>> Create(ProductsCreateDto request);
        Task<PagedResult<ProductReadDto>> GetPagingProduct (ProductPagingDto request);
        Task<List<ProductReadDto>> GetListProduct();
    }
}

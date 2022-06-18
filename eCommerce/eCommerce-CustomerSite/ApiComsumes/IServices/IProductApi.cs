using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.ProductDto;

namespace eCommerce_CustomerSite.ApiComsumes.IServices
{
    public interface IProductApi
    {
        Task<ApiResult<bool>> Create(ProductCreateDto request);
        Task<PagedResult<ProductReadDto>> GetPagingProduct(ProductPagingDto request);
    }
}

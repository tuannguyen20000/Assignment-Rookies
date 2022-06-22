using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;

namespace eCommerce_CustomerSite.ApiComsumes.IServices
{
    public interface IProductApi
    {
        Task<ApiResult<bool>> Create(ProductCreateDto request);
        Task<PagedResult<ProductReadDto>> GetPagingProduct(ProductPagingDto request);
        Task<ApiResult<ProductReadDto>> GetById(int Id);
        Task<ApiResult<bool>> AddComment(int Id, ProductRatingCreateDto request);

    }
}

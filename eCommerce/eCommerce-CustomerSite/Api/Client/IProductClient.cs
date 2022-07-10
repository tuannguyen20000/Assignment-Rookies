using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;
using Refit;

namespace eCommerce_CustomerSite.Api.Client
{
    public interface IProductClient
    {
        [Get("/api/Products/get-paging-product")]
        Task<PagedResult<ProductReadDto>> GetPagingProductAsync(ProductPagingDto request);
        [Get("/api/Products/get-by-id/{Id}")]
        Task<ApiResult<ProductReadDto>> GetByIdAsync( int Id);
        [Post("/api/Products/{Id}/add-comment")]
        Task<ApiResult<bool>> AddCommentAsync(int Id, ProductRatingCreateDto request);
    }
}

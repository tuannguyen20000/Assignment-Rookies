using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;

namespace eCommerce_CustomerSite.ApiComsumes.IServices
{
    public interface IProductApi
    {
        Task<ApiResult<bool>> CreateAsync(ProductCreateDto request);
        Task<PagedResult<ProductReadDto>> GetPagingProductAsync(ProductPagingDto request);
        Task<ApiResult<ProductReadDto>> GetByIdAsync(int Id);
        Task<ApiResult<bool>> AddCommentAsync(int Id, ProductRatingCreateDto request);
        Task<ApiResult<AvgRatingDto>> GetAvgRatingByIdAsync(int Id);
    }
}

using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductImage;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;

namespace eCommerce_Backend.Application.IServices
{
    public interface IProductService
    {
        Task<PagedResult<ProductReadDto>> GetPagingAsync(ProductPagingDto request);
        Task<List<ProductReadDto>> GetListAsync();
        Task<ApiResult<bool>> CreateAsync(ProductCreateDto request);
        Task<ApiResult<bool>> UpdateAsync(int Id,ProductUpdateDto request);
        Task<ApiResult<ProductReadDto>> GetByIdAsync(int Id);
        Task<ApiResult<bool>> SoftDeleteAsync(int Id);

        // Images
        Task<ApiResult<bool>> AddImageAsync(int Id, ProductImageCreateDto request);
        Task<ApiResult<bool>> RemoveImageAsync(int imageId);
        Task<ApiResult<bool>> UpdateImageAsync(int imageId, ProductImageUpdateDto request);
        Task<List<ProductImageDto>> GetListImageByProductIdAsync(int Id);
        Task<ApiResult<ProductImageDto>> GetImageByIdAsync(int imageId);

        // Categories
        Task<ApiResult<bool>> CategoryAssignAsync(int Id, CategoryAssignDto request);

        // Comments
        Task<ApiResult<bool>> AddCommentAsync(int Id, ProductRatingCreateDto request);
        Task<ApiResult<AvgRatingDto>> GetAvgRatingByIdAsync(int Id);
    }
}

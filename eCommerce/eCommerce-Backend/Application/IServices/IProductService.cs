using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductImage;

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

        // Images
        Task<ApiResult<bool>> AddImage(int Id, ProductImageCreateDto request);
        Task<ApiResult<bool>> RemoveImage(int imageId);
        Task<ApiResult<bool>> UpdateImage(int imageId, ProductImageUpdateDto request);
        Task<List<ProductImageDto>> GetListImageByProductId(int Id);
        Task<ApiResult<ProductImageDto>> GetImageById(int imageId);
        Task<ApiResult<bool>> CategoryAssign(int Id, CategoryAssignDto request);
    }
}

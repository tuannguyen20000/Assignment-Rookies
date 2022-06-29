using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;

namespace eCommerce_CustomerSite.Models
{
    public class ProductPagingRatingVM
    {
        public PagedResult<ProductReadDto> ProductsPaging { get; set; }
        public AvgRatingDto Rating { get; set; }
    }
}

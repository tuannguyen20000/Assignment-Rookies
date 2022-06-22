using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;

namespace eCommerce_CustomerSite.Models
{
    public class ProductRatingVM
    {
        public ProductReadDto Product { get; set; }

        public ProductRatingCreateDto Rating { get; set; }
    }
}

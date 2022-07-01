using eCommerce_SharedViewModels.EntitiesDto.Product.ProductImage;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;
using eCommerce_SharedViewModels.Enums;

namespace eCommerce_SharedViewModels.EntitiesDto.Product
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Status Status { get; set; }
        public string ThumbnailImage { get; set; }
        public int? avrRating { get; set; }
        public int? countComment { get; set; }


        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public List<ProductRatingDto> Comments { get; set; }
        public List<ProductImageDto> SubImages { get; set; }
    }
}

using eCommerce_SharedViewModels.Enums;

namespace eCommerce_SharedViewModels.EntitiesDto.Product
{
    public class ProductReadDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Status Status { get; set; }
        public string ThumbnailImage { get; set; }
    }
}

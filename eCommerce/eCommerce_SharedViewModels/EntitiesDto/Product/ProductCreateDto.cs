using eCommerce_SharedViewModels.Enums;
using Microsoft.AspNetCore.Http;

namespace eCommerce_SharedViewModels.EntitiesDto.Product
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int ProductQuantity { get; set; }
        public decimal Price { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}

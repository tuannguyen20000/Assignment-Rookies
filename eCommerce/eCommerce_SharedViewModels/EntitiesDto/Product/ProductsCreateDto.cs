namespace eCommerce_SharedViewModels.EntitiesDto.ProductDto
{
    public class ProductsCreateDto
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImagessURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}

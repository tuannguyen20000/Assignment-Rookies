using eCommerce_SharedViewModels.Enums;

namespace eCommerce_Backend.Data.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }   
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Status Status { get; set; }

        public List<ProductRatings> ProductRatings { get; set; }
        public List<ProductInCategory> ProductInCategory { get; set; }
        public List<ProductImages> ProductImages { get; set; }
    }
}

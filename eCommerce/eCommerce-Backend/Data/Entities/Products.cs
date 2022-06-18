using eCommerce_SharedViewModels.Enums;

namespace eCommerce_Backend.Data.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public int CategoiesId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }   
        public string ImagessURL { get; set; }  
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Status Status { get; set; }

        public Categories Categoies { get; set; }
    }
}

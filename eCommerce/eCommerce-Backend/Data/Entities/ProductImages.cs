namespace eCommerce_Backend.Data.Entities
{
    public class ProductImages
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public DateTime DateCreated { get; set; }
        public long FileSize { get; set; }

        public Products Products { get; set; }
    }
}

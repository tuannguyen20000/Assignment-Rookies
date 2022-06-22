namespace eCommerce_Backend.Data.Entities
{
    public class ProductInCategory
    {
        public int ProductsId { get; set; }
        public Products Products { get; set; }
        public int CategoriesId { get; set; }
        public Categories Categories { get; set; }
    }
}

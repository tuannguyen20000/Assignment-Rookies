namespace eCommerce_Backend.Data.Entities
{
    public class Carts
    {
        public int Id { get; set; }    
        public int ProductsId { get; set; }
        public string UsersId { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public DateTime DateCreated { get; set; }

        public Products Products { get; set; }
        public Users Users { get; set; }
    }
}

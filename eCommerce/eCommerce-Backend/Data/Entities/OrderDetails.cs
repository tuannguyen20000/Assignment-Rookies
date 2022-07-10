namespace eCommerce_Backend.Data.Entities
{
    public class OrderDetails
    {
        public int OrdersId { set; get; }
        public int ProductsId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Orders Orders { get; set; }

        public Products Products { get; set; }
    }
}

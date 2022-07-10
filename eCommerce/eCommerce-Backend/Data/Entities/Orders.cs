using eCommerce_SharedViewModels.Enums;

namespace eCommerce_Backend.Data.Entities
{
    public class Orders
    {
        public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        public string UsersId { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public StatusOrder Status { set; get; }

        public List<OrderDetails> OrderDetails { get; set; }

        public Users Users { get; set; }
    }
}

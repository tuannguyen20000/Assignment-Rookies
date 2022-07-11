using eCommerce_SharedViewModels.EntitiesDto.Order.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Order
{
    public class OrderCreateDto
    {
        public string UsersId { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }

        // Order Detail
        public List<OrderDetailReadDto> orderDetails {get;set;}
        public int ProductsId { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
    }
}

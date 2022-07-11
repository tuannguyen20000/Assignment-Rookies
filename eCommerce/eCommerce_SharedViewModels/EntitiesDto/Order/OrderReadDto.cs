using eCommerce_SharedViewModels.EntitiesDto.Order.OrderDetail;
using eCommerce_SharedViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Order
{
    public class OrderReadDto
    {
        public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        public string UsersId { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public StatusOrder Status { set; get; }

        public List<OrderDetailReadDto> OrderDetails { get; set; }

    }
}

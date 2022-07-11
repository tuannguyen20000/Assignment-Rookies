using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Order.OrderDetail
{
    public class OrderDetailReadDto
    {
        public int OrdersId { set; get; }
        public int ProductsId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
    }
}

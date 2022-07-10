using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Cart
{
    public class CartCreateDto
    {
        public int ProductsId { get; set; }
        public string UsersId { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
    }
}

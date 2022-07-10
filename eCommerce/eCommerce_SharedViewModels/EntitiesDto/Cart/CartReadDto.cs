using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Cart
{
    public class CartReadDto
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string UsersId { get; set; }
        public string Description { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public DateTime DateCreated { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Cart
{
    public class CartUpdateDto
    {
        public string userId { get; set; }
        public int quantity { get; set; }
    }
}

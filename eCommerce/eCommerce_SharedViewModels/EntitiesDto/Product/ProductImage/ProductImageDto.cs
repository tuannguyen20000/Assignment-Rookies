using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Product.ProductImage
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public DateTime DateCreated { get; set; }
        public long FileSize { get; set; }
    }
}

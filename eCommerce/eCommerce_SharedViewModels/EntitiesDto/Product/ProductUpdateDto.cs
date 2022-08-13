using eCommerce_SharedViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Product
{
    public class ProductUpdateDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ProductQuantity { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        public string[] Categories { get; set; }
    }
}

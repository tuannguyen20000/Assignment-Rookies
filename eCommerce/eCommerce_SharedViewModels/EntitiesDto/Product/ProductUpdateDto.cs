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
        public string Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}

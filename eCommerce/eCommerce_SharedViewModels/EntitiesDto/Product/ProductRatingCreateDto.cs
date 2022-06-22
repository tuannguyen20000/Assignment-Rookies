using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Product
{
    public class ProductRatingCreateDto
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int Rating { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

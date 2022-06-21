using eCommerce_SharedViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Categories
{
    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string ThumbnailImage { get; set; }
    }
}

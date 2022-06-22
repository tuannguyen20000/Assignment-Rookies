using eCommerce_SharedViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Product
{
    public class CategoryAssignDto
    {
        public List<SelectItem> Categories { get; set; } = new List<SelectItem>();
    }
}

using eCommerce_SharedViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Order
{
    public class OrderPagingDto : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}

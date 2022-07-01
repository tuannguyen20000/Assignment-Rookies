using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Login
{
    public class ResponseAuth
    {
        public string accessToken { get; set; }
        public string user { get; set; }
        public string isInRole { get; set; }
    }
}

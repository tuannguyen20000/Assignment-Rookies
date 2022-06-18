using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Enities
{
    public class RefreshToken
    {
        public string UserName { get; set; }

        public DateTimeOffset Expiration { get; set; }

        public DateTimeOffset? ConsumedTime { get; set; }

        public string TokenHash { get; set; }
    }
}

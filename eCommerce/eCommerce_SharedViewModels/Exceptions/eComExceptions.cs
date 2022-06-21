using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.Exceptions
{
    public class eComExceptions : Exception
    {
        public eComExceptions()
        {

        }
        public eComExceptions(string message) : base(message)
        {

        }

        public eComExceptions(string message, Exception inner) : base(message, inner)
        {

        }
    }
}

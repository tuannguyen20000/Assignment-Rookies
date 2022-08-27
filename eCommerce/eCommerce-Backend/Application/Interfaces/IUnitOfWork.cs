using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_Backend.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Order
        {
            get;
        }
        int Save();
    }
}

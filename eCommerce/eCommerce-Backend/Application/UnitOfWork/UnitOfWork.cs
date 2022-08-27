using eCommerce_Backend.Application.Interfaces;
using eCommerce_Backend.Application.TypeRepository;
using eCommerce_Backend.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_Backend.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private eCommerceDbContext dbContext;
        public UnitOfWork(eCommerceDbContext context)
        {
            dbContext = context;
            Order = new OrderRepository(dbContext);
        }
        public IOrderRepository Order 
        {   
            get;
            private set;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public int Save()
        {
            return dbContext.SaveChanges();
        }
    }
}

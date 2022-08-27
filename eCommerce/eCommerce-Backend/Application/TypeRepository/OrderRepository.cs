using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;

namespace eCommerce_Backend.Application.TypeRepository
{
    public class OrderRepository : GenericRepository <Orders>, IOrderRepository
    {
        public OrderRepository(eCommerceDbContext dbContext) : base(dbContext) { }
    }
}


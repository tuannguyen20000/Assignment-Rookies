using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using EntityFrameworkCore.Triggered;

namespace eCommerce_Backend.Data.Triggers
{

    public class UserIdTrigger : IAfterSaveTrigger<Orders>, IDisposable
    {
        private readonly eCommerceDbContext _dbContext;
        public UserIdTrigger(eCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AfterSave(ITriggerContext<Orders> context, CancellationToken cancellationToken)
        {
/*            if (context.ChangeType == ChangeType.Added && context.Entity.UsersId == null)
            {
                var count = _dbContext.Orders.Count(x => x.OrderDate.Date == context.Entity.OrderDate.Date);
                var orders = await _dbContext.Orders.FindAsync(new object[] { context.Entity.Id }, cancellationToken: cancellationToken);
                orders.UsersId = orders.OrderDate.Date.ToString("ddMMyyyy");
                orders.UsersId += "IDUser";
                orders.UsersId += count.ToString("0000");
                await _dbContext.SaveChangesAsync(cancellationToken);
            }*/
        }


        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using EntityFrameworkCore.Triggered;

namespace eCommerce_Backend.Data.Triggers
{

    public class ProductIdTrigger : IAfterSaveTrigger<Products>, IDisposable
    {
        private readonly eCommerceDbContext _dbContext;
        public ProductIdTrigger(eCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AfterSave(ITriggerContext<Products> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {

            }
        }


        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

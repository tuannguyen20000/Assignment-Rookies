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
/*                var count = _dbContext.Products.Count(x => x.UpdatedDate.Date == context.Entity.UpdatedDate.Date);
                var product = await _dbContext.Products.FindAsync(new object[] { context.Entity.Id }, cancellationToken: cancellationToken);
                product.Id = product.UpdatedDate.Date.ToString("ddMMyyyy");
                product.Id += "IDProduct";
                product.Id += count.ToString("0000");
                await _dbContext.SaveChangesAsync(cancellationToken);*/
            }
        }


        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

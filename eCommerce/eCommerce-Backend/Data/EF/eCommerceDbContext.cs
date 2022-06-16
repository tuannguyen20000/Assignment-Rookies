using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_Backend.Data.EF
{
    public class eCommerceDbContext : IdentityDbContext
    {
        public eCommerceDbContext(DbContextOptions<eCommerceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

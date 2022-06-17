using eCommerce_Backend.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_Backend.Data.EF
{
    public class eCommerceDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public eCommerceDbContext(DbContextOptions<eCommerceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class AppUser : IdentityUser
    {
    }
}

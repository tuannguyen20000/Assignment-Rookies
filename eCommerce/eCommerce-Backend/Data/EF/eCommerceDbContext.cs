using eCommerce_Backend.Data.Configuration;
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
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<CategoryImages> CategoryImages { get; set; }
        public DbSet<ProductInCategory> ProductInCategory { get; set; }
        public DbSet<ProductRatings> ProductRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductInCategoryCf());
        }
    }
}

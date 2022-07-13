using eCommerce_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_Backend.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().HasData(
                new Products()
                {
                    Id = 343434,
                    ProductName = "Product name 1",
                    ProductQuantity = 1,
                    CreatedDate = DateTime.Now.Date,
                    UpdatedDate = DateTime.Now.Date,
                    Description = "Des 1",
                    Price = 12313,
                    Status = 0,                   
                });
        }
    }
}

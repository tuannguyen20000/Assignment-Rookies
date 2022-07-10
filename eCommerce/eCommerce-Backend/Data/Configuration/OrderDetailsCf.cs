using eCommerce_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce_Backend.Data.Configuration
{
    public class OrderDetailsCf : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => new { x.OrdersId, x.ProductsId });

            builder.HasOne(x => x.Orders).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrdersId);
            builder.HasOne(x => x.Products).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductsId);
        }
    }
}

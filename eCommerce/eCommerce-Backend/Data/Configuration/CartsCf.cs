using eCommerce_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce_Backend.Data.Configuration
{
    public class CartsCf : IEntityTypeConfiguration<Carts>
    {
        public void Configure(EntityTypeBuilder<Carts> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Products).WithMany(x => x.Carts).HasForeignKey(x => x.ProductsId);
            builder.HasOne(x => x.Users).WithMany(x => x.Carts).HasForeignKey(x => x.UsersId);
        }
    }
}

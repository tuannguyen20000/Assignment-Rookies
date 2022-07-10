using eCommerce_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce_Backend.Data.Configuration
{
    public class OrdersCf : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.OrderDate);
            builder.Property(x => x.ShipEmail).IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ShipAddress).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ShipName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ShipPhoneNumber).IsRequired().HasMaxLength(200);

            builder.HasOne(x => x.Users).WithMany(x => x.Orders).HasForeignKey(x => x.UsersId);
        }
    }
}

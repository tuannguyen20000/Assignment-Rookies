using eCommerce_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce_Backend.Data.Configuration
{
    public class ProductInCategoryCf : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(t => new { t.CategoriesId, t.ProductsId });

            builder.ToTable("ProductInCategory");

            builder.HasOne(t => t.Products).WithMany(pc => pc.ProductInCategory)
                .HasForeignKey(pc => pc.ProductsId);

            builder.HasOne(t => t.Categories).WithMany(pc => pc.ProductInCategory)
              .HasForeignKey(pc => pc.CategoriesId);
        }
    }
}

using eCommerce_Backend.Data.EF;
using eCommerce_xUniTest.DummyData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace eCommerce_xUniTest.Data
{
    public abstract class SQLiteContext
    {
        public DbConnection _connection;
        public DbContextOptions<eCommerceDbContext> _contextOptions;
        public SQLiteContext()
        {
            _connection = new SqliteConnection("Filename =:memory:");
            _connection.Open();
            _contextOptions = new DbContextOptionsBuilder<eCommerceDbContext>()
              .UseSqlite(_connection)
              .Options;
            var dbContext = new eCommerceDbContext(_contextOptions);
            if (dbContext.Database.EnsureCreated())
            {
                dbContext.Products.AddRange(ProductFakeData.ListProductst());
                dbContext.ProductInCategory.AddRange(ProductInCategoryFakeData.ListProductInCategory());
                dbContext.Categories.AddRange(CategoryFakeData.ListCategory());
                dbContext.SaveChangesAsync();
            }
        }
        public eCommerceDbContext CreateContext() => new eCommerceDbContext(_contextOptions);
    }
}

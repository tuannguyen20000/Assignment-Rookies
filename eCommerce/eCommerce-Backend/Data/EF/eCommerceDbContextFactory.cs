using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eCommerce_Backend.Data.EF
{
    public class eCommerceDbContextFactory : IDesignTimeDbContextFactory<eCommerceDbContext>
    {
        public eCommerceDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("eCommerceDb");
            var optionBuiler = new DbContextOptionsBuilder<eCommerceDbContext>();
            optionBuiler.UseSqlServer(connectionString);
            return new eCommerceDbContext(optionBuiler.Options);
        }
    }
}

using eCommerce_Backend.Application.Common;
using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Application.Services;
using eCommerce_Backend.Data.EF;
using eCommerce_xUniTest.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_xUniTest.Services
{
    public class ProductServiceTest : SQLiteContext
    {
        private readonly eCommerceDbContext _dbContext;
        public ProductServiceTest()
        {
            _dbContext = CreateContext();
        }

        private IProductService GetSqlLiteService()
        {
            var mockFileStorage = new Mock<IFileStorage>();
            return new ProductService(_dbContext, mockFileStorage.Object);
        }


        [Fact]
        public async void GetListProduct_WhenCall_ReturnListProduct()
        {
            IProductService service = GetSqlLiteService();
            var result = await service.GetListAsync();
            Assert.Equal(2,result.Count);
            Assert.NotNull(result);
        }
    }
}

using eCommerce_Backend.Application.Common;
using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Application.Services;
using eCommerce_Backend.Data.EF;
using eCommerce_xUniTest.Data;
using eCommerce_xUniTest.DummyData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eCommerce_SharedViewModels.Utilities.Constants.SystemConstants;

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

        // GET LIST
        [Fact]
        public async void GetListProduct_WhenCall_ReturnListProduct()
        {
            IProductService service = GetSqlLiteService();
            var result = await service.GetListAsync();
            Assert.Equal(2,result.Count);
            Assert.NotNull(result);
        }

        // GET LIST PAGING
        [Fact]
        public async void GetListProductPaging_WhenCall_ReturnListProductPaging()
        {
            IProductService service = GetSqlLiteService();
            var result = await service.GetPagingAsync(ProductFakeData.PagingItemProduct());
            Assert.Equal(2, result.Items.Count);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetListProductPagingByKeyWord_WhenCall_ReturnListProductPagingFilterByProductName()
        {
            IProductService service = GetSqlLiteService();
            var result = await service.GetPagingAsync(ProductFakeData.PagingItemProductKeyWord());
            Assert.Equal("Dep ca sau", result.Items.First().ProductName);
            Assert.Single(result.Items);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetListProductPagingByKeyWord_WhenCall_ReturnListProductPagingEmptyItem()
        {
            var pagingProductDto = new ProductPagingDto()
            {
                CategoriesId = null,
                PageSize = 12,
                PageIndex = 1,
                Keyword = "abc"
            };
            IProductService service = GetSqlLiteService();
            var result = await service.GetPagingAsync(pagingProductDto);
            Assert.Empty(result.Items);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetListProductPagingByCategory_WhenCall_ReturnListProductFilterByCategory()
        {
            var pagingProductDto = new ProductPagingDto()
            {
                CategoriesId = 10000,
                PageSize = 12,
                PageIndex = 1,
                Keyword = null
            };
            IProductService service = GetSqlLiteService();
            var result = await service.GetPagingAsync(pagingProductDto);
            Assert.Equal("Dep ca sau", result.Items.First().ProductName);
            Assert.Single(result.Items);
            Assert.NotNull(result);
        }

        // CREATE PRODUCT
        [Fact]
        public async void CreateProduct_WhenCall_ReturnTrue()
        {
            IProductService service = GetSqlLiteService();
            var result = await service.CreateAsync(ProductFakeData.createItemProduct());
            Assert.True(result.IsSuccessed);
        }

        // UPDATE PRODUCT
        [Fact]
        public async void UpdateProduct_WhenCall_ReturnTrue()
        {
            IProductService service = GetSqlLiteService();
            var result = await service.UpdateAsync(1,ProductFakeData.UpdatetemProduct());
            Assert.True(result.IsSuccessed);
        }

        [Fact]
        public async void UpdateProduct_WhenCall_ReturnProductExist()
        {
            var updateProductDto = new ProductUpdateDto()
            {
                Description = "update Description",
                Price = 100000,
                ProductName = "Dep ca sau",
            };
            IProductService service = GetSqlLiteService();
            var result = await service.UpdateAsync(1, updateProductDto);
            Assert.Equal(ErrorMessage.ProductNameExists, result.Message);
        }

        [Fact]
        public async void UpdateProduct_WhenCall_ReturnProductNotFound()
        {
            var updateProductDto = new ProductUpdateDto()
            {
                Description = "update Description",
                Price = 100000,
                ProductName = "Dep ca sau",
            };
            IProductService service = GetSqlLiteService();
            var result = await service.UpdateAsync(9999, updateProductDto);
            Assert.Equal(ErrorMessage.ProductNotFound, result.Message);
        }

        // SOFT DELETE PRODUCT
        [Fact]
        public async void SoftDeleteProduct_WhenCall_ReturnTrue()
        {
            IProductService service = GetSqlLiteService();
            var result = await service.SoftDeleteAsync(1);
            Assert.True(result.IsSuccessed);
        }
    }
}

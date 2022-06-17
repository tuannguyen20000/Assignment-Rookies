using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Controllers;
using eCommerce_SharedViewModels.Common;
using eCommerce_xUniTest.DummyData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace eCommerce_xUniTest.Controller
{
    public class Product
    {
        [Fact]
        public async Task GetListProduct_WhenCalled_ReturnsOkResult()
        {
            /// Arrange
            var shopService = new Mock<IProductService>();
            shopService.Setup(x => x.GetListProduct()).ReturnsAsync(ProductFakeData.GetProduct());
            var controller = new ProductsController(shopService.Object);
            /// Act
            var result = await controller.GetListProduct() as OkObjectResult;
            // /// Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<List<ProductReadDto>>(model.Value);
            Assert.Equal(2, value.Count());
        }

        [Fact]
        public async Task CreateProduct_WhenCalled_ReturnsResultSuccessed()
        {
            /// Arrange
            var shopService = new Mock<IProductService>();
            shopService.Setup(x => x.Create(ProductFakeData.itemProduct())).ReturnsAsync(new ApiSuccessResult<bool>());
            var controller = new ProductsController(shopService.Object);
            /// Act
            var result = await controller.Create(ProductFakeData.itemProduct());
            // Assert
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<ApiSuccessResult<bool>>(model.Value);
            Assert.Equal("", value.errorMessage);
            Assert.Equal("True", value.IsSuccessed.ToString());
        }
    }
}
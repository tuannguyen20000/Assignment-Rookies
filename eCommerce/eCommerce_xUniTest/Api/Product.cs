using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Controllers;
using eCommerce_SharedViewModels.EntitiesDto.ProductDto;
using eCommerce_xUniTest.DummyData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace eCommerce_xUniTest.Controller
{
    public class Product
    {
        [Fact]
        public async Task GetAllListProduct_WhenCalled_ReturnsOkResult()
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
    }
}
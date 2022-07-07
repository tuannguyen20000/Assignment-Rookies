using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Controllers;
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
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.GetListAsync()).ReturnsAsync(ProductFakeData.GetProduct);
            var controller = new ProductsController(productService.Object);
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
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.CreateAsync(It.IsAny<ProductCreateDto>())).ReturnsAsync(new ApiSuccessResult<bool>());
            var controller = new ProductsController(productService.Object);
            /// Act
            var result = await controller.Create(ProductFakeData.createItemProduct());
            // Assert
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<ApiSuccessResult<bool>>(model.Value);
            Assert.Null(value.errorMessage);
            Assert.Equal("True", value.IsSuccessed.ToString());
        }

        [Fact]
        public async Task UpdateProduct_WhenCalled_ReturnsResultSuccessed()
        {
            /// Arrange
            var productId = ProductFakeData.GetProduct().Select(x => x.Id).FirstOrDefault();
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.UpdateAsync(productId, It.IsAny<ProductUpdateDto>())).ReturnsAsync(new ApiSuccessResult<bool>());
            var controller = new ProductsController(productService.Object);
            /// Act
            var result = await controller.Update(productId, ProductFakeData.UpdatetemProduct());
            // Assert
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<ApiSuccessResult<bool>>(model.Value);
            Assert.Null(value.errorMessage);
            Assert.Equal("True", value.IsSuccessed.ToString());
        }


        [Fact]
        public async Task GetProductPaging_WhenCalled_ReturnsPagingProduct()
        {
            /// Arrange
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.GetPagingAsync(It.IsAny<ProductPagingDto>())).ReturnsAsync(ProductFakeData.PageResultProductRead);
            var controller = new ProductsController(productService.Object);
            /// Act
            var result = await controller.GetPagingProduct(ProductFakeData.PagingItemProduct());
            // /// Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<PagedResult<ProductReadDto>>(model.Value);
            Assert.Equal(2, value.Items.Count());

        }
    }
}
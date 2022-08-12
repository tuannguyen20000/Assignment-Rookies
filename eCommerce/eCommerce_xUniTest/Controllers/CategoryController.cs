using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Controllers;
using eCommerce_xUniTest.DummyData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace eCommerce_xUniTest.Api
{
    public class CategoryController
    {
        [Fact]
        public async Task GetListCategory_WhenCalled_ReturnsOkResult()
        {
            /// Arrange
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.GetListAsync()).ReturnsAsync(CategoryFakeData.GetCategory);
            var controller = new CategoriesController(categoryService.Object);
            /// Act
            var result = await controller.GetListCategory() as OkObjectResult;
            // /// Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<List<CategoryReadDto>>(model.Value);
            Assert.Equal(3, value.Count());
        }

        [Fact]
        public async Task CreateCategory_WhenCalled_ReturnsResultSuccessed()
        {
            /// Arrange
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.CreateAsync(It.IsAny<CategoryCreateDto>())).ReturnsAsync(new ApiSuccessResult<bool>());
            var controller = new CategoriesController(categoryService.Object);
            /// Act
            var result = await controller.Create(CategoryFakeData.createItemCategory());
            // Assert
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<ApiSuccessResult<bool>>(model.Value);
            Assert.Null(value.Message);
            Assert.Equal("True", value.IsSuccessed.ToString());
        }

        [Fact]
        public async Task UpdateCategory_WhenCalled_ReturnsResultSuccessed()
        {
            /// Arrange
            var categoryId = CategoryFakeData.GetCategory().Select(x => x.Id).FirstOrDefault();
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.UpdateAsync(categoryId, It.IsAny<CategoryUpdateDto>())).ReturnsAsync(new ApiSuccessResult<bool>());
            var controller = new CategoriesController(categoryService.Object);
            /// Act
            var result = await controller.Update(categoryId, CategoryFakeData.UpdatetemCategory());
            // Assert
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<ApiSuccessResult<bool>>(model.Value);
            Assert.Null(value.Message);
            Assert.Equal("True", value.IsSuccessed.ToString());
        }


        [Fact]
        public async Task GetCategoryPaging_WhenCalled_ReturnsPagingCategory()
        {
            /// Arrange
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.GetPagingAsync(It.IsAny<CategoryPagingDto>())).ReturnsAsync(CategoryFakeData.PageResultCategoryRead);
            var controller = new CategoriesController(categoryService.Object);
            /// Act
            var result = await controller.GetPagingCategory(CategoryFakeData.PagingItemCategory());
            // /// Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<PagedResult<CategoryReadDto>>(model.Value);
            Assert.Equal(3, value.Items.Count());

        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkResult()
        {
            /// Arrange
            var categoryId = CategoryFakeData.GetCategory().Select(x => x.Id).FirstOrDefault();
            var category = CategoryFakeData.GetCategory().First(x => x.Id == categoryId);
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new ApiSuccessResult<CategoryReadDto>(category));
            var controller = new CategoriesController(categoryService.Object);
            /// Act
            var result = await controller.GetById(categoryId) as OkObjectResult;
            // /// Assert
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<ApiSuccessResult<CategoryReadDto>>(model.Value);
            Assert.NotNull(value.ResultObj);
        }

    }
}

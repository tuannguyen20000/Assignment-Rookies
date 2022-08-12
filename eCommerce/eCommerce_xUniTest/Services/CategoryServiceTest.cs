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
    public class CategoryServiceTest : SQLiteContext
    {
        private readonly eCommerceDbContext _dbContext;
        public CategoryServiceTest()
        {
            _dbContext = CreateContext();
        }

        private ICategoryService GetSqlLiteService()
        {
            var mockFileStorage = new Mock<IFileStorage>();
            return new CategoryService(_dbContext, mockFileStorage.Object);
        }

        // GET LIST
        [Fact]
        public async void GetListCategory_WhenCall_ReturnListCategory()
        {
            ICategoryService service = GetSqlLiteService();
            var result = await service.GetListAsync();
            Assert.Equal(2, result.Count);
            Assert.NotNull(result);
        }

        // GET LIST PAGING
        [Fact]
        public async void GetListCategoryPaging_WhenCall_ReturnListCategoryPaging()
        {
            ICategoryService service = GetSqlLiteService();
            var result = await service.GetPagingAsync(CategoryFakeData.PagingItemCategory());
            Assert.Equal(2, result.Items.Count);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetListCategoryPagingByKeyWord_WhenCall_ReturnListCategoryPagingFilterByCategoryName()
        {
            ICategoryService service = GetSqlLiteService();
            var result = await service.GetPagingAsync(CategoryFakeData.PagingItemCategory());
            Assert.Equal("test category name 1", result.Items.First().CategoryName);
            Assert.Equal(2,result.Items.Count());
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetListCategoryPagingByKeyWord_WhenCall_ReturnListCategoryPagingEmptyItem()
        {
            var pagingCategoryDto = new CategoryPagingDto()
            {
                CategoriesId = null,
                PageSize = 12,
                PageIndex = 1,
                Keyword = "abc"
            };
            ICategoryService service = GetSqlLiteService();
            var result = await service.GetPagingAsync(pagingCategoryDto);
            Assert.Empty(result.Items);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetListCategoryPagingByCategory_WhenCall_ReturnListCategoryFilterByCategory()
        {
            var pagingCategoryDto = new CategoryPagingDto()
            {
                CategoriesId = 10000,
                PageSize = 12,
                PageIndex = 1,
                Keyword = null
            };
            ICategoryService service = GetSqlLiteService();
            var result = await service.GetPagingAsync(pagingCategoryDto);
            Assert.Equal("test category name 1", result.Items.First().CategoryName);
            Assert.Single(result.Items);
            Assert.NotNull(result);
        }

        // CREATE CATEGORY
        [Fact]
        public async void CreateCategory_WhenCall_ReturnTrue()
        {
            ICategoryService service = GetSqlLiteService();
            var result = await service.CreateAsync(CategoryFakeData.createItemCategory());
            Assert.True(result.IsSuccessed);
        }

        // UPDATE CATEGORY
        [Fact]
        public async void UpdateCategory_WhenCall_ReturnTrue()
        {
            ICategoryService service = GetSqlLiteService();
            var result = await service.UpdateAsync(10001, CategoryFakeData.UpdatetemCategory());
            Assert.True(result.IsSuccessed);
        }

        [Fact]
        public async void UpdateCategory_WhenCall_ReturnCategoryExist()
        {
            var updateCategoryDto = new CategoryUpdateDto()
            {
                Description = "update Description",
                CategoryName = "test category name 1",
            };
            ICategoryService service = GetSqlLiteService();
            var result = await service.UpdateAsync(10001, updateCategoryDto);
            Assert.Equal(ErrorMessage.CategoryNameExists, result.Message);
        }

        [Fact]
        public async void UpdateCategory_WhenCall_ReturnCategoryNotFound()
        {
            var updateCategoryDto = new CategoryUpdateDto()
            {
                Description = "update Description",
                CategoryName = "test category name 1",
            };
            ICategoryService service = GetSqlLiteService();
            var result = await service.UpdateAsync(9999, updateCategoryDto);
            Assert.Equal(ErrorMessage.CategoryNotFound, result.Message);
        }

        // SOFT DELETE CATEGORY
        [Fact]
        public async void SoftDeleteCategory_WhenCall_ReturnTrue()
        {
            ICategoryService service = GetSqlLiteService();
            var result = await service.SoftDeleteAsync(10001);
            Assert.True(result.IsSuccessed);
        }
    }
}

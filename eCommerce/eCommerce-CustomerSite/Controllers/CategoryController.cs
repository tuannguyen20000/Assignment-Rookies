using eCommerce_CustomerSite.Api.IServices;
using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_CustomerSite.Models;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_CustomerSite.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IProductApi _productApi;
        public CategoryController(ICategoryApi categoryApi, IProductApi productApi)
        {
            _categoryApi = categoryApi;
            _productApi = productApi;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 8)
        {
            var request = new CategoryPagingDto()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _categoryApi.GetPagingCategory(request);
            return View(data);
        }

        public async Task<IActionResult> ProductsOfCategory(int CategoryId, int pageIndex = 1, int pageSize = 12)
        {
            var products = await _productApi.GetPagingProduct(new ProductPagingDto()
            {
                CategoriesId = CategoryId,
                PageIndex = pageIndex,
                PageSize = pageSize,              
            });
            var category = await _categoryApi.GetById(CategoryId);
            return View(new ProductsOfCategoryVM()
            {
                Category = category.ResultObj,
                Products = products
            });
        }
    }
}

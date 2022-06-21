using eCommerce_CustomerSite.Api.IServices;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_CustomerSite.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryApi _categoryApi;
        public CategoryController(ICategoryApi categoryApi)
        {
            _categoryApi = categoryApi;
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
    }
}

using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApi _productApi;
        public ProductController(IProductApi productApi)
        {
            _productApi = productApi;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 1)
        {
            var request = new ProductPagingDto()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _productApi.GetPagingProduct(request);
            return View(data);
        }
    }
}

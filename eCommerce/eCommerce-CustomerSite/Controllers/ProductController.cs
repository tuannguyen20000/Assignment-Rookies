using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_CustomerSite.Models;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;
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

        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 12)
        {
            var request = new ProductPagingDto()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoriesId = categoryId
            };
            var data = await _productApi.GetPagingProduct(request);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {
            var result = await _productApi.GetById(Id);
            if (!result.IsSuccessed)
            {
                TempData["error"] = result.Message;
                return View();
            }
            return View(new ProductRatingVM()
            {
                Product = result.ResultObj,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int Id, ProductRatingVM request)
        {
            var result = await _productApi.GetById(Id);
            if (!result.IsSuccessed)
            {
                TempData["error"] = result.Message;
                return View();
            }
            var rating = await _productApi.AddComment(Id, request.Rating);
            return View(new ProductRatingVM() { 
                Product = result.ResultObj,
            });
        }
    }
}

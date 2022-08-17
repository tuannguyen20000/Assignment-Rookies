using eCommerce_CustomerSite.Api.Client;
using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_CustomerSite.Models;
using eCommerce_SharedViewModels.EntitiesDto.Order;
using eCommerce_SharedViewModels.EntitiesDto.Order.OrderDetail;
using eCommerce_SharedViewModels.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using System.Security.Claims;
using static eCommerce_CustomerSite.Common.GetCart;

namespace eCommerce_CustomerSite.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductApi _productApi;
        private readonly IOrderClient _orderClient = RestService.For<IOrderClient>("https://localhost:7211");

        public OrderController(IProductApi productApi)
        {
            _productApi = productApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderCreateDto request)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            var sessionUser = HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var currentCart = await GetCartAsync(userId, session, sessionUser);
            request.orderDetails = currentCart.Select(x => new OrderDetailReadDto()
            {
                ProductsId = x.ProductId,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList();
            foreach(var item in request.orderDetails)
            {
                var product = await _productApi.GetByIdAsync(item.ProductsId);
                if(product.ResultObj.ProductQuantity == 0)
                {
                    TempData["warning"] = $"{product.ResultObj.ProductName} is out of stock";
                    return View();
                }
            }
            request.UsersId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = await _orderClient.CreateAsync(request);
            if(data != 0)
            {
                HttpContext.Session.Remove(SystemConstants.SESSION_CART);
                TempData["success"] = "Your order has been placed!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
    }
}

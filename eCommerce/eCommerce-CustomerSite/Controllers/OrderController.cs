using eCommerce_CustomerSite.Api.Client;
using eCommerce_CustomerSite.Models;
using eCommerce_SharedViewModels.EntitiesDto.Order;
using eCommerce_SharedViewModels.EntitiesDto.Order.OrderDetail;
using eCommerce_SharedViewModels.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using System.Security.Claims;

namespace eCommerce_CustomerSite.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartClient _cartClient = RestService.For<ICartClient>("https://localhost:7211");
        private readonly IOrderClient _orderClient = RestService.For<IOrderClient>("https://localhost:7211");

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderCreateDto request)
        {        
            var currentCart = await GetCartAsync();
            request.orderDetails = currentCart.Select(x => new OrderDetailReadDto()
            {
                ProductsId = x.ProductId,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList();
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


        private async Task<List<CartItemVM>> GetCartAsync()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            var sessionUser = HttpContext.Session.GetString(SystemConstants.AppSettings.Token);

            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (!string.IsNullOrEmpty(session))
            {
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            }
            if (!string.IsNullOrEmpty(sessionUser) || !string.IsNullOrEmpty(userId))
            {
                var data = await _cartClient.GetListCartAsync(userId);
                var cartUser = data.Select(x => new CartItemVM()
                {
                    Image = x.Image,
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price,
                    ProductId = x.ProductsId,
                    Quantity = x.Quantity
                }).ToList();
                currentCart = cartUser;
            }
            return currentCart;
        }

    }
}

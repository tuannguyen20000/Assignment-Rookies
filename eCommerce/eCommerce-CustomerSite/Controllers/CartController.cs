using eCommerce_CustomerSite.Api.Client;
using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_CustomerSite.Models;
using eCommerce_SharedViewModels.EntitiesDto.Cart;
using eCommerce_SharedViewModels.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using System.Security.Claims;

namespace eCommerce_CustomerSite.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApi _productApi;
        private readonly ICartClient _cartClient = RestService.For<ICartClient>("https://localhost:7211");
        public CartController(IProductApi productApi)
        {
            _productApi = productApi;
        }

        [HttpGet]
        public async Task<IActionResult> GetListCart()
        {
            var currentCart = await GetCartAsync();
            return Ok(currentCart);
        }

        public async Task<IActionResult> UpdateCart(int Id, int quantity)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentCart = await GetCartAsync();
            // update current cart
            foreach (var item in currentCart)
            {
                if (item.ProductId == Id)
                {
                    if (quantity == 0)
                    {
                        currentCart.Remove(item);
                        if (!string.IsNullOrEmpty(userId))
                        {
                            await _cartClient.DeleteAsync(Id, item.Quantity);   
                        }
                        break;
                    }
                    item.Quantity = quantity;
                }
            }
            HttpContext.Session.SetString(SystemConstants.SESSION_CART, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }


        public async Task<IActionResult> AddProductToCart(int Id, int Quantity)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _productApi.GetByIdAsync(Id);
            var currentCart = await GetCartAsync();
            if (currentCart.Any(x => x.ProductId == Id))
            {
                Quantity = currentCart.First(x => x.ProductId == Id).Quantity + Quantity;
                currentCart.Remove(currentCart.First(x => x.ProductId == Id));
            }
            var cartItem = new CartItemVM()
            {
                ProductId = Id,
                Description = product.ResultObj.Description,
                Image = product.ResultObj.ThumbnailImage,
                Name = product.ResultObj.ProductName,
                Price = product.ResultObj.Price,
                Quantity = Quantity
            };
            if (!string.IsNullOrEmpty(userId))
            {
                var cartUser = new CartCreateDto()
                {
                    ProductsId = Id,
                    UsersId = userId,
                    Price = product.ResultObj.Price,
                    Quantity = Quantity,
                };
               await _cartClient.CreateAsync(cartUser);
            }

            currentCart.Add(cartItem);
            HttpContext.Session.SetString(SystemConstants.SESSION_CART, JsonConvert.SerializeObject(currentCart));
            return Json(new { success = true, responseText = "The product has been added to the cart" });
        }

        public IActionResult Detail()
        {
            return View();
        }

        public async Task<List<CartItemVM>> GetCartAsync()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            var sessionUser = HttpContext.Session.GetString(SystemConstants.AppSettings.Token);

            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (!string.IsNullOrEmpty(session))
            {
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            }
            if(!string.IsNullOrEmpty(sessionUser) || !string.IsNullOrEmpty(userId))
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

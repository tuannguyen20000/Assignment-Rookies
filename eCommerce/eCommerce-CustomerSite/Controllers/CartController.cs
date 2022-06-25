using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_CustomerSite.Models;
using eCommerce_SharedViewModels.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerce_CustomerSite.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApi _productApi;
        public CartController(IProductApi productApi)
        {
            _productApi = productApi;
        }

        [HttpGet]
        public IActionResult GetListCart()
        {
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            return Ok(currentCart);
        }

        public IActionResult UpdateCart(int id, int quantity)
        {
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            foreach (var item in currentCart)
            {
                if (item.ProductId == id)
                {
                    if (quantity == 0)
                    {
                        currentCart.Remove(item);
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
            var product = await _productApi.GetById(Id);
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            var cartItem = new CartItemVM()
            {
                ProductId = Id,
                Description = product.ResultObj.Description,
                Image = product.ResultObj.ThumbnailImage,
                Name = product.ResultObj.ProductName,
                Price = product.ResultObj.Price,
                Quantity = Quantity
            };
            currentCart.Add(cartItem);
            HttpContext.Session.SetString(SystemConstants.SESSION_CART, JsonConvert.SerializeObject(currentCart));
            return Json(new { success = true, responseText = "The product has been added to the cart" });
        }
    }
}

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

        public async Task<IActionResult> AddProductToCart(int Id)
        {
            var product = await _productApi.GetById(Id);
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            int quantity = 1;
            if (currentCart.Any(x => x.ProductId == Id))
            {
                quantity = currentCart.First(x => x.ProductId == Id).Quantity + 1;
            }
            var cartItem = new CartItemVM()
            {
                ProductId = Id,
                Description = product.ResultObj.Description,
                Image = product.ResultObj.ThumbnailImage,
                Name = product.ResultObj.ProductName,
                Price = product.ResultObj.Price,
                Quantity = quantity
            };
            currentCart.Add(cartItem);
            HttpContext.Session.SetString(SystemConstants.SESSION_CART, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }
    }
}

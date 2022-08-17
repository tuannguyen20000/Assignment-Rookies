using eCommerce_CustomerSite.Api.Client;
using eCommerce_CustomerSite.Models;
using Newtonsoft.Json;
using Refit;

namespace eCommerce_CustomerSite.Common
{
    public static class GetCart
    {
        private static readonly ICartClient _cartClient = RestService.For<ICartClient>("https://localhost:7211");
        public static async Task<List<CartItemVM>> GetCartAsync(string userId, string session, string sessionUser)
        {
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
                    Quantity = x.Quantity,
                    ProductQuantity = x.ProductQuantity,
                }).ToList();
                currentCart = cartUser;
            }
            return currentCart;
        }
    }
}

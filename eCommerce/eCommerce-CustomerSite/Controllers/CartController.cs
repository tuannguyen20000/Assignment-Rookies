﻿using eCommerce_CustomerSite.Api.Client;
using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_CustomerSite.Common;
using eCommerce_CustomerSite.Models;
using eCommerce_SharedViewModels.EntitiesDto.Cart;
using eCommerce_SharedViewModels.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using System.Security.Claims;
using static eCommerce_CustomerSite.Common.GetCart;

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
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            var sessionUser = HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var currentCart = await GetCartAsync(userId, session, sessionUser);
            return Ok(currentCart);
        }

        public async Task<IActionResult> UpdateCart(int Id, int quantity)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            var sessionUser = HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var currentCart = await GetCartAsync(userId, session, sessionUser);
            var request = new CartUpdateDto()
            {
                quantity = quantity,
                userId = userId,
            };
            // update current cart
            foreach (var item in currentCart)
            {
                if (item.ProductId == Id)
                {
                    if (quantity == 0)
                    {
                        currentCart.Remove(item);
                        // Delete if empty cart user
                        if (!string.IsNullOrEmpty(userId))
                        {
                            await _cartClient.DeleteAsync(Id, userId);   
                        }
                        break;
                    }
                    item.Quantity = quantity;
                    // Update cart user
                    if (!string.IsNullOrEmpty(userId))
                    {
                        await _cartClient.UpdateAsync(Id, request);
                    }     
                }
            }
            HttpContext.Session.SetString(SystemConstants.SESSION_CART, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }


        public async Task<IActionResult> AddProductToCart(int Id, int Quantity)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var session = HttpContext.Session.GetString(SystemConstants.SESSION_CART);
            var sessionUser = HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var product = await _productApi.GetByIdAsync(Id);
            if (product.ResultObj.ProductQuantity.Equals(0))
            {
                return Json(new { success = false, responseText = "The product is out of stock" });
            }
            var currentCart = await GetCartAsync(userId, session, sessionUser);
            if (currentCart.Any(x => x.ProductId == Id))
            {              
                Quantity += currentCart.First(x => x.ProductId == Id).Quantity;
                if(Quantity > product.ResultObj.ProductQuantity)
                    return Json(new { success = false, responseText = "Quantity is not enough" });
                if (!string.IsNullOrEmpty(userId))
                    await _cartClient.DeleteAsync(Id, userId);

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
            return Json(new { success = true, responseText = "1 item added to cart" });
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}

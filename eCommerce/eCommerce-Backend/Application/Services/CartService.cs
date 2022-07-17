using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Cart;
using eCommerce_SharedViewModels.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_Backend.Application.Services
{
    public class CartService : ICartService
    {
        private readonly eCommerceDbContext _dbContext;
        public CartService(eCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateAsync(CartCreateDto request)
        {
            using (_dbContext)
            {
                var cart = new Carts()
                {
                    ProductsId = request.ProductsId,
                    UsersId = request.UsersId,
                    Quantity = request.Quantity,
                    Price = request.Price,
                    DateCreated = DateTime.Now.Date,
                };
                _dbContext.Add(cart);
                return await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(int Id, string userId)
        {
            using (_dbContext)
            {
                var products = await _dbContext.Carts.Where(x => x.ProductsId == Id && x.UsersId == userId).FirstOrDefaultAsync();
                if (products == null) throw new eComExceptions($"Cannot find an product with id {Id}");
                _dbContext.Carts.Remove(products);
                return await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<CartReadDto>> GetListAsync(string UserId)
        {
            using (_dbContext)
            {
                var data = _dbContext.Carts.AsQueryable();
                if(!string.IsNullOrEmpty(UserId))
                {
                    data = data.Where(x => x.UsersId == UserId);
                }
                var result = await data
                    .Include(x => x.Users)
                    .Include(x => x.Products).ThenInclude(x=>x.ProductImages)
                    .Select(x => new CartReadDto()
                    {
                        Id = x.Id,
                        UsersId = x.UsersId,
                        DateCreated = x.DateCreated,
                        ProductsId = x.ProductsId,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        Image = x.Products.ProductImages.Where(x => x.IsDefault == true).Select(x => x.ImagePath).FirstOrDefault(),
                        Description = x.Products.Description,
                        Name = x.Products.ProductName,      
                        ProductQuantity = x.Products.ProductQuantity,
                    }).ToListAsync();
                return result;
            }
        }

        public async Task<int> UpdateAsync(int Id, CartUpdateDto request)
        {
            using (_dbContext)
            {
                var products = await _dbContext.Carts.Where(x => x.ProductsId == Id && x.UsersId == request.userId).FirstOrDefaultAsync();
                if (products == null) throw new eComExceptions($"Cannot find an product with id {Id}");
                products.Quantity = request.quantity;
                _dbContext.Carts.Update(products);
                return await _dbContext.SaveChangesAsync();
            }
        }
    }
}

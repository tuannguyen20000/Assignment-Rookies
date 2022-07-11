﻿using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.EntitiesDto.Order;

namespace eCommerce_Backend.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly eCommerceDbContext _dbContext;
        public OrderService(eCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateAsync(OrderCreateDto request)
        {
            using (_dbContext)
            {
                var order = new Orders()
                {
                    OrderDate = DateTime.Now.Date,
                    UsersId = request.UsersId,
                    ShipAddress = request.ShipAddress,
                    ShipEmail = request.ShipEmail,
                    ShipName = request.ShipName,
                    ShipPhoneNumber = request.ShipPhoneNumber,
                    Status = eCommerce_SharedViewModels.Enums.StatusOrder.InProgress,
                };
                // add order
                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();
                // update order detail
                order.OrderDetails = request.orderDetails.Select(x => new OrderDetails()
                {
                    ProductsId = x.ProductsId,
                    OrdersId = order.Id,
                    Price = x.Price,
                    Quantity = x.Quantity,
                }).ToList();
                // remove cart of user
                if (!string.IsNullOrEmpty(request.UsersId))
                {
                    _dbContext.Carts.RemoveRange(_dbContext.Carts.Where(x => x.UsersId == request.UsersId));
                    await _dbContext.SaveChangesAsync();
                }
                _dbContext.Orders.Update(order);       
                return await _dbContext.SaveChangesAsync();
            }
        }


    }
}
using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Order;
using eCommerce_SharedViewModels.EntitiesDto.Order.OrderDetail;
using eCommerce_SharedViewModels.Enums;
using Microsoft.EntityFrameworkCore;
using static eCommerce_SharedViewModels.Utilities.Constants.SystemConstants;

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
                    Status = StatusOrder.InProgress,
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
                // update product quantity
                foreach(var item in request.orderDetails)
                {
                    var product = await _dbContext.Products.FindAsync(item.ProductsId);
                    product.ProductQuantity -= item.Quantity;
                    _dbContext.Products.Update(product);
                }
                _dbContext.Orders.Update(order);       
                return await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<OrderReadDto>> GetPagingAsync(OrderPagingDto request)
        {
            using (_dbContext)
            {
                var query = _dbContext.Orders.Include(x => x.OrderDetails).AsQueryable();
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.Users.UserName.Contains(request.Keyword));
                }
                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new OrderReadDto()
                    {
                        Id = x.Id,
                        OrderDate = x.OrderDate,
                        ShipAddress = x.ShipAddress,
                        ShipEmail = x.ShipEmail,
                        OrderDetails = x.OrderDetails.Select(x=> new OrderDetailReadDto() 
                            {OrdersId = x.OrdersId, Price = x.Price, ProductsId =x.ProductsId, Quantity = x.Quantity }).ToList(),
                        ShipName = x.ShipName,
                        ShipPhoneNumber = x.ShipPhoneNumber,
                        Status = x.Status,
                        UsersId = x.UsersId
                    }).ToListAsync();
                var pagedResult = new PagedResult<OrderReadDto>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedResult;
            }
        }

        public async Task<ApiResult<bool>> UpdateStatusAsync(int Id, OrderUpdateDto request)
        {
            var data = await _dbContext.Orders.FindAsync(Id);
            if (data == null)
                return new ApiErrorResult<bool>(ErrorMessage.ProductNotFound);
            data.Status = request.Status;            
            _dbContext.Orders.Update(data);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}

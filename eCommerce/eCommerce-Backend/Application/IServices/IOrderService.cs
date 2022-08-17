using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Order;

namespace eCommerce_Backend.Application.IServices
{
    public interface IOrderService
    {
        Task<int> CreateAsync(OrderCreateDto request);
        Task<ApiResult<bool>> UpdateStatusAsync(int Id, OrderUpdateDto request);
        Task<PagedResult<OrderReadDto>> GetPagingAsync(OrderPagingDto request);
    }
}

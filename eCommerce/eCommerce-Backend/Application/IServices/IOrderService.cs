using eCommerce_SharedViewModels.EntitiesDto.Order;

namespace eCommerce_Backend.Application.IServices
{
    public interface IOrderService
    {
        Task<int> CreateAsync(OrderCreateDto request);
    }
}

using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Cart;

namespace eCommerce_Backend.Application.IServices
{
    public interface ICartService
    {
        Task<List<CartReadDto>> GetListAsync(string UserId);
        Task<int> CreateAsync(CartCreateDto request);
        Task<int> UpdateAsync(int Id, CartUpdateDto request);
        Task<int> DeleteAsync(int Id, string userId);
    }
}

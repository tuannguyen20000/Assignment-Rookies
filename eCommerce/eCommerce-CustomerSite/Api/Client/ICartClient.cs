using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Cart;
using Refit;

namespace eCommerce_CustomerSite.Api.Client
{

    public interface ICartClient
    {
        [Get("/api/Carts/get-list-cart")]
        Task<List<CartReadDto>> GetListCartAsync(string UserId);

        [Headers("Content-Type: application/json")]
        [Post("/api/Carts/create-cart")]
        Task<int> CreateAsync([Body] CartCreateDto request);

        [Headers("Content-Type: application/json")]
        [Delete("/api/Carts/delete-cart/{Id}/{quantity}")]
        Task<int> DeleteAsync(int Id, int quantity);
    }
}

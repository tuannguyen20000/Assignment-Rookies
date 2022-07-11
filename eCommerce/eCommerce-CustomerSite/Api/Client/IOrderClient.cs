using eCommerce_SharedViewModels.EntitiesDto.Order;
using Refit;

namespace eCommerce_CustomerSite.Api.Client
{
    public interface IOrderClient
    {
        [Headers("Content-Type: application/json")]
        [Post("/api/Orders/create-order")]
        Task<int> CreateAsync([Body] OrderCreateDto request);
    }
}

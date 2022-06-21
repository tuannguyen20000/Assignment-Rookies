using eCommerce_CustomerSite.Api.Common;
using eCommerce_CustomerSite.Api.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using eCommerce_SharedViewModels.Utilities.Constants;

namespace eCommerce_CustomerSite.Api.Services
{
    public class CategoryApi : HttpService, ICategoryApi
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryApi(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<CategoryReadDto>> GetPagingCategory(CategoryPagingDto request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var data = await GetAsync<PagedResult<CategoryReadDto>>($"https://localhost:7211/api/Categories/get-paging-category?&" +
                $"pageIndex={request.PageIndex}&pageSize={request.PageSize}", session);
            return data;
        }
    }
}

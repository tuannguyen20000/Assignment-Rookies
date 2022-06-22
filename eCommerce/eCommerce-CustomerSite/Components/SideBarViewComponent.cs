using eCommerce_CustomerSite.Api.IServices;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_CustomerSite.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;
        public SideBarViewComponent(ICategoryApi categoryApi)
        {
            _categoryApi = categoryApi;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _categoryApi.GetList();
            return View(data);
        }
    }
}

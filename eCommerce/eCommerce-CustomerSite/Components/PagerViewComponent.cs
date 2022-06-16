using eCommerce_SharedViewModels.Common;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_CustomerSite.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}

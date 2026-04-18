using Microsoft.AspNetCore.Mvc;

namespace Rentaly.WebUI.ViewComponents
{
    public class FAQViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
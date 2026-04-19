using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents
{
    public class ListHeaderViewComponent : ViewComponent
    {
      
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
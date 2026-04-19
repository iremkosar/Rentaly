using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents
{
    public class HowItWorkViewComponent : ViewComponent
    {
        private readonly IHowItWorkService _howItWorkService;

        public HowItWorkViewComponent(IHowItWorkService howItWorkService)
        {
            _howItWorkService = howItWorkService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {  
            var values=await _howItWorkService.TGetListAsync();
            return View(values);
        }
    }
}

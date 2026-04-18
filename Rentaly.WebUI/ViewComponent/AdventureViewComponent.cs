using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents
{
    public class AdventureViewComponent : ViewComponent
    {
        private readonly IAdventureService _adventureService;

        public AdventureViewComponent(IAdventureService adventureService)
        {
            _adventureService = adventureService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var values=await _adventureService.TGetListAsync();
            return View(values);
        }
    }
}
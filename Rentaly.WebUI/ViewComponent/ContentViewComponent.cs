using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.ViewComponents
{
    public class ContentViewComponent : ViewComponent
    {
        private readonly ILocationService _locationService;

        public ContentViewComponent(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var locations = await _locationService.TGetListAsync();
            return View(locations);
        }
    }
}
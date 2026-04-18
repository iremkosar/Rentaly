using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents
{
    public class FeaturesViewComponent : ViewComponent
    {
        private readonly IFeatureService _featureService;

        public FeaturesViewComponent(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var features = await _featureService.TGetListAsync();
            return View(features);
        }
    }
}
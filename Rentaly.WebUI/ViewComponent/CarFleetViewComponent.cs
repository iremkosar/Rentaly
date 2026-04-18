using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents
{
    public class CarFleetViewComponent : ViewComponent
    {
        private readonly ICarService _carService;

        public CarFleetViewComponent(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var car = await _carService.TGetAllCarsWithCategoryAsync();
            return View(car);
        }
    }
}
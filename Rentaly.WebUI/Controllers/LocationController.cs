using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _locationService.TGetListAsync();
            return View(values);
        }

        public IActionResult CreateLocation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(Location location)
        {
            await _locationService.TInsertAsync(location);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var values = await _locationService.TGetByIdAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Location location)
        {
            await _locationService.TUpdateAsync(location);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _locationService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
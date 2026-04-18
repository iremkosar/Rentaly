using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public async Task<IActionResult> Index()
        {
            var features= await _featureService.TGetListAsync();
            return View(features);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Feature feature)
        {
            await _featureService.TInsertAsync(feature);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var feature=await _featureService.TGetByIdAsync(id);
            return View(feature);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Feature feature)
        {
            await _featureService.TUpdateAsync(feature);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _featureService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }
     
    }
}

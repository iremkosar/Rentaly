using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
    public class AdventureController : Controller
    {
        private readonly IAdventureService _adventureService;

        public AdventureController(IAdventureService adventureService)
        {
            _adventureService = adventureService;
        }

        public async Task<IActionResult> Index()
        {
           var adventure=await _adventureService.TGetListAsync();
            return View(adventure);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Adventure adventure)
        {
          await _adventureService.TInsertAsync(adventure);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var value=await _adventureService.TGetByIdAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Adventure adventure)
        {
            await _adventureService.TUpdateAsync(adventure);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int id)
        {
            await _adventureService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}

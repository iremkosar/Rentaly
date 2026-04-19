using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using System.Threading.Tasks;

namespace Rentaly.WebUI.Controllers
{
    public class HowItWorkController : Controller
    {
        private readonly IHowItWorkService _howItWorkService;

        public HowItWorkController(IHowItWorkService howItWorkService)
        {
            _howItWorkService = howItWorkService;
        }

        public async Task<IActionResult> Index()
        {
            var values=await _howItWorkService.TGetListAsync();
            return View(values);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HowItWork howItWork)
        {
            await _howItWorkService.TInsertAsync(howItWork);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var values=await _howItWorkService.TGetByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> Update(HowItWork howItWork)
        {
            await _howItWorkService.TUpdateAsync(howItWork);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _howItWorkService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}

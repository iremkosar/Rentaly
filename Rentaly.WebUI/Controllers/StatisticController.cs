using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public async Task<IActionResult> Index()
        {
            var values= await _statisticService.TGetListAsync();
            return View(values);
        }
       
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Statistic statistic)
        {
            await _statisticService.TInsertAsync(statistic);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var values = await _statisticService.TGetByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Statistic statistic)
        {
            await _statisticService.TUpdateAsync(statistic);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _statisticService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}

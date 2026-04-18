using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents
{
    public class StatsViewComponent : ViewComponent
    {
        private readonly IStatisticService _statisticService;

        public StatsViewComponent(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values=await _statisticService.TGetListAsync();
            return View(values);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using System.Threading.Tasks;

namespace Rentaly.WebUI.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IActionResult> Index()
        {
            var values=await _faqService.TGetListAsync();
            return View(values);
           
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Faq faq)
        {
            await _faqService.TInsertAsync(faq);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var values=await _faqService.TGetByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Faq faq)
        {
            await _faqService.TUpdateAsync(faq);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _faqService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}

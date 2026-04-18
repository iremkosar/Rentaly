using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _testimonialService.TGetListAsync();
            return View(values);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Testimonial testimonial)
        {
           await _testimonialService.TInsertAsync(testimonial);
           return RedirectToAction("Index");
        }
      
        public async Task<IActionResult> Update(int id)
        {
            var values=await _testimonialService.TGetByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Testimonial testimonial)
        {
            await _testimonialService.TUpdateAsync(testimonial);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _testimonialService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }

            
    }
}

using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents
{
    public class TestimonialsViewComponent : ViewComponent
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialsViewComponent(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var values= await _testimonialService.TGetListAsync();
            return View(values);
            
        }
    }
}
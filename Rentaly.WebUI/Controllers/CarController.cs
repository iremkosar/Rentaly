using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using System.Threading.Tasks;

namespace Rentaly.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;
        private readonly IBranchService _branchService;
        private readonly IBrandService _brandService;

        public CarController(ICarService carService, ICategoryService categoryService, IBranchService branchService, IBrandService brandService)
        {
            _carService = carService;
            _categoryService = categoryService;
            _branchService = branchService;
            _brandService = brandService;
        }

        public async Task<IActionResult> CarList()
        {
            var values = await _carService.TGetAllCarsWithCategoryAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            ViewBag.Categories = new SelectList(await _categoryService.TGetListAsync(), "CategoryId", "CategoryName");
            ViewBag.Brands = new SelectList(await _brandService.TGetListAsync(), "BrandId", "BrandName");           
            //ViewBag.Models = new SelectList(_modelService.GetAll(), "ModelId", "ModelName");
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName");
            return View(); 
          
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car)
        {
            car.CarId = 0;
            await _carService.TInsertAsync(car);
            return RedirectToAction("CarList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var car = await _carService.TGetByIdAsync(id);
            ViewBag.Categories = new SelectList(await _categoryService.TGetListAsync(), "CategoryId", "CategoryName", car.CategoryId);
            ViewBag.Brands = new SelectList(await _brandService.TGetListAsync(), "BrandId", "BrandName", car.BrandId);
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName", car.BranchId);
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(Car car)
        {
            await _carService.TUpdateAsync(car);
            return RedirectToAction("CarList");
        }

     
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.TDeleteAsync(id);
            return RedirectToAction("CarList");
        }
    }
}



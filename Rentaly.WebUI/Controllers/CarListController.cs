using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
    public class CarListController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IReservationService _reservationService;

        public CarListController(ICarService carService, ICategoryService categoryService, IBrandService brandService, IReservationService reservationService)
        {
            _carService = carService;
            _categoryService = categoryService;
            _brandService = brandService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index(
            List<int>? categoryId,
            List<int>? brandId,
            List<int>? seatCount,
            decimal? minPrice,
            decimal? maxPrice,
            DateTime? pickUpDateTime,
            DateTime? returnDateTime,
            string? pickUpLocation,
            string? dropOffLocation)
        {
            var cars = await _carService.TGetAllCarsWithCategoryAsync();

            if (categoryId != null && categoryId.Any())
                cars = cars.Where(x => categoryId.Contains(x.CategoryId)).ToList();

            if (brandId != null && brandId.Any())
                cars = cars.Where(x => brandId.Contains(x.BrandId)).ToList();

            if (seatCount != null && seatCount.Any())
                cars = cars.Where(x => seatCount.Contains(x.SeatCount)).ToList();

            if (minPrice.HasValue)
                cars = cars.Where(x => x.DailyPrice >= minPrice.Value).ToList();

            if (maxPrice.HasValue)
                cars = cars.Where(x => x.DailyPrice <= maxPrice.Value).ToList();

            // Tarih seçildiyse müsait olmayan araçları çıkar
            if (pickUpDateTime.HasValue && returnDateTime.HasValue)
            {
                var allReservations = await _reservationService.TGetListAsync();
                var unavailableCarIds = allReservations
                    .Where(r => r.Status != "İptal" &&
                                r.PickUpDateTime < returnDateTime.Value &&
                                r.ReturnDateTime > pickUpDateTime.Value)
                    .Select(r => r.CarId)
                    .Distinct()
                    .ToList();

                cars = cars.Where(x => !unavailableCarIds.Contains(x.CarId)).ToList();
            }

            ViewBag.Categories = await _categoryService.TGetListAsync();
            ViewBag.Brands = await _brandService.TGetListAsync();
            ViewBag.SelectedCategories = categoryId ?? new List<int>();
            ViewBag.SelectedBrands = brandId ?? new List<int>();
            ViewBag.SelectedSeats = seatCount ?? new List<int>();
            ViewBag.MinPrice = minPrice ?? 0;
            ViewBag.MaxPrice = maxPrice ?? 10000;
            ViewBag.PickUpDateTime = pickUpDateTime;
            ViewBag.ReturnDateTime = returnDateTime;
            ViewBag.PickUpLocation = pickUpLocation;
            ViewBag.DropOffLocation = dropOffLocation;

            return View(cars);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;

        public ReservationController(IReservationService reservationService, ICarService carService, ICustomerService customerService)
        {
            _reservationService = reservationService;
            _carService = carService;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int carId)
        {
            var car = await _carService.TGetByIdAsync(carId);
            if (car == null) return NotFound();
            ViewBag.Car = car;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            string name, string surname, string email, string phone,
            string identityNumber, string drivingLicenseNumber, DateTime drivingLicenseDate,
            int carId, string pickUpLocation, string dropOffLocation,
            DateTime pickUpDateTime, DateTime returnDateTime, string? message)
        {
            // 1. Araç müsait mi?
            bool isAvailable = await _reservationService.TIsCarAvailableAsync(carId, pickUpDateTime, returnDateTime);

            if (!isAvailable)
            {
                var car = await _carService.TGetByIdAsync(carId);
                ViewBag.Car = car;
                ViewBag.Error = "Seçilen tarihlerde bu araç müsait değil.";
                return View();
            }

            // 2. Müşteriyi kaydet
            var customerDto = new CreateCustomerDto
            {
                Name = name,
                Surname = surname,
                Email = email,
                Phone = phone,
                IdentityNumber = identityNumber,
                DrivingLicenseNumber = drivingLicenseNumber,
                DrivingLicenseDate = drivingLicenseDate
            };
            await _customerService.TInsertAsync(customerDto);

            // 3. Email ile kaydedilen müşteriyi çek
            var savedCustomer = await _customerService.TGetByEmailAsync(email);

            // 4. Toplam fiyat hesapla
            var selectedCar = await _carService.TGetByIdAsync(carId);
            int days = (returnDateTime - pickUpDateTime).Days;
            if (days < 1) days = 1;

            // 5. Rezervasyonu kaydet
            var reservation = new Reservation
            {
                CarId = carId,
                CustomerId = savedCustomer.CustomerId,
                PickUpLocation = pickUpLocation,
                DropOffLocation = dropOffLocation,
                PickUpDateTime = pickUpDateTime,
                ReturnDateTime = returnDateTime,
                Message = message,
                TotalPrice = days * selectedCar.DailyPrice,
                CreatedAt = DateTime.Now,
                Status = "Beklemede"
            };
            await _reservationService.TInsertAsync(reservation);

            ViewBag.Car = selectedCar;
            ViewBag.Success = true;
            ViewBag.ReservationId = reservation.ReservationId;
            return View();
        }

        public async Task<IActionResult> Success(int id)
        {
            var reservation = await _reservationService.TGetByIdAsync(id);
            return View(reservation);
        }
    }
}
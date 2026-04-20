using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;
using Rentaly.WebUI.Services;

namespace Rentaly.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;
        private readonly ILocationService _locationService;
        private readonly IEmailService _emailService;

        public ReservationController(IReservationService reservationService, ICarService carService, ICustomerService customerService, ILocationService locationService, IEmailService emailService)
        {
            _reservationService = reservationService;
            _carService = carService;
            _customerService = customerService;
            _locationService = locationService;
            _emailService = emailService;
        }
        [HttpGet]
        public async Task<IActionResult> Create(int carId, DateTime? pickUpDateTime, DateTime? returnDateTime, string? pickUpLocation, string? dropOffLocation)
        {
            var car = await _carService.TGetByIdAsync(carId);
            if (car == null) return NotFound();

            ViewBag.Car = car;
            ViewBag.Locations = await _locationService.TGetListAsync();
            ViewBag.PickUpDateTime = pickUpDateTime?.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.ReturnDateTime = returnDateTime?.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.PickUpLocation = pickUpLocation;
            ViewBag.DropOffLocation = dropOffLocation;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            string name, string surname, string email, string phone,
            string identityNumber, string drivingLicenseNumber, DateTime drivingLicenseDate,
            int carId, string pickUpLocation, string dropOffLocation,
            DateTime pickUpDateTime, DateTime returnDateTime, string? message)
        {
            bool isAvailable = await _reservationService.TIsCarAvailableAsync(carId, pickUpDateTime, returnDateTime);
            var selectedCar = await _carService.TGetByIdAsync(carId);

            if (!isAvailable)
            {
                ViewBag.Car = selectedCar;
                ViewBag.Locations = await _locationService.TGetListAsync();
                ViewBag.Error = "Seçilen tarihlerde bu araç müsait değil.";
                return View();
            }

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

            var savedCustomer = await _customerService.TGetByEmailAsync(email);

            int days = (returnDateTime - pickUpDateTime).Days;
            if (days < 1) days = 1;

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
            ViewBag.Locations = await _locationService.TGetListAsync();
            ViewBag.Success = true;
            ViewBag.ReservationId = reservation.ReservationId;
            return View();
        }


        public async Task<IActionResult> Approve(int id)
        {
            var reservation = await _reservationService.TGetByIdAsync(id);
            var customer = await _customerService.TGetByIdAsync(reservation.CustomerId);

            string discountCode = "RENT-" + Guid.NewGuid().ToString("N").Substring(0, 5).ToUpper();

            reservation.Status = "Onaylandı";
            reservation.DiscountCode = discountCode;
            await _reservationService.TUpdateAsync(reservation);

            await _emailService.SendReservationConfirmationAsync(
                customer.Email,
                customer.Name + " " + customer.Surname,
                reservation.ReservationId,
                discountCode,
                reservation.PickUpDateTime,
                reservation.ReturnDateTime
            );

            return RedirectToAction("AdminIndex");
        }


        public async Task<IActionResult> AdminIndex()
        {
            var reservations = await _reservationService.TGetReservationsWithDetailsAsync();
            return View(reservations);
        }
        public async Task<IActionResult> Cancel(int id)
        {
            var reservation = await _reservationService.TGetByIdAsync(id);
            reservation.Status = "İptal";
            await _reservationService.TUpdateAsync(reservation);
            return RedirectToAction("AdminIndex");
        }
    }
}
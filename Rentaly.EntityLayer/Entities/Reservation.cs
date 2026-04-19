using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }

        public DateTime PickUpDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }

        public string? Message { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Beklemede";
    }
}

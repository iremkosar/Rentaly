using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface IReservationService:IGenericService<Reservation>
    {
        Task<List<Reservation>> TGetReservationsByCarIdAsync(int carId);
        Task<List<Reservation>> TGetReservationsWithDetailsAsync();
        Task<bool> TIsCarAvailableAsync(int carId, DateTime pickUp, DateTime returnDate);
    }
}

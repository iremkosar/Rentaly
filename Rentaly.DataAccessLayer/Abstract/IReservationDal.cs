using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.Abstract
{
    public interface IReservationDal : IGenericDal<Reservation>
    {
        Task<List<Reservation>> TGetReservationsByCarIdAsync(int carId);
        Task<List<Reservation>> TGetReservationsWithDetailsAsync();
    }
}
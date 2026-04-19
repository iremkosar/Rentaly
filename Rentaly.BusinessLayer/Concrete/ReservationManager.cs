using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Concrete
{
    public class ReservationManager : IReservationService
    {
        private readonly IReservationDal _reservationDal;

        public ReservationManager(IReservationDal reservationDal)
        {
            _reservationDal = reservationDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _reservationDal.DeleteAsync(id);
        }

        public async Task<Reservation> TGetByIdAsync(int id)
        {
            return await _reservationDal.GetByIdAsync(id);
        }

        public async Task<List<Reservation>> TGetListAsync()
        {
            return await _reservationDal.GetListAsync();
        }

        public async Task TInsertAsync(Reservation entity)
        {
            await _reservationDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Reservation entity)
        {
            await _reservationDal.UpdateAsync(entity);
        }

        public async Task<List<Reservation>> TGetReservationsByCarIdAsync(int carId)
        {
            return await _reservationDal.TGetReservationsByCarIdAsync(carId);
        }

        public async Task<List<Reservation>> TGetReservationsWithDetailsAsync()
        {
            return await _reservationDal.TGetReservationsWithDetailsAsync();
        }

        public async Task<bool> TIsCarAvailableAsync(int carId, DateTime pickUp, DateTime returnDate)
        {
            var reservations = await _reservationDal.TGetReservationsByCarIdAsync(carId);

            bool hasConflict = reservations
                .Where(x => x.Status != "İptal")
                .Any(x => x.PickUpDateTime < returnDate && x.ReturnDateTime > pickUp);

            return !hasConflict;
        }
    }
}
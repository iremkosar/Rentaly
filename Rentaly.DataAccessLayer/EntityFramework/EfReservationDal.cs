using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.EntityFramework
{
    public class EfReservationDal : IReservationDal
    {
        private readonly RentalyContext _context;

        public EfReservationDal(RentalyContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Reservation entity)
        {
            await _context.Reservations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation entity)
        {
            _context.Reservations.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Reservations.FindAsync(id);
            if (entity != null)
            {
                _context.Reservations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<List<Reservation>> GetListAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<List<Reservation>> TGetReservationsByCarIdAsync(int carId)
        {
            return await _context.Reservations
                .Where(x => x.CarId == carId)
                .ToListAsync();
        }

        public async Task<List<Reservation>> TGetReservationsWithDetailsAsync()
        {
            return await _context.Reservations
                .Include(x => x.Car)
                    .ThenInclude(x => x.Brand)
                .Include(x => x.Customer)
                .ToListAsync();
        }
    }
}
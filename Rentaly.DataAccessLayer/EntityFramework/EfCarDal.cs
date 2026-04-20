using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.DataAccessLayer.EntityFramework
{
    public class EfCarDal : GenericRepository<Car>, ICarDal
    {
        private readonly RentalyContext _context;

        public EfCarDal(RentalyContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<List<Car>> GetAllCarsWithCategoryAsync()
        {
            return await _context.Cars
        .Include(x => x.Category)
        .Include(x => x.Brand)
        .Include(x => x.Branch)
        .AsNoTracking()
        .Where(x => x.IsActive)
        .Distinct()
        .ToListAsync();
        }
    }
}

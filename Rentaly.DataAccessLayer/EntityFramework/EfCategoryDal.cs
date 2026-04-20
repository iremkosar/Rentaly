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
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly RentalyContext _context;
        public EfCategoryDal(RentalyContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategoriesWithCarsAsync()
        {
            return await _context.Categories
                .Include(c => c.Cars)
                .ToListAsync();
        }
    }
}

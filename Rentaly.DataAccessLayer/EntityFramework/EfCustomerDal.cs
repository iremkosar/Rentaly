using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.DtoLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.EntityFramework
{
    public class EfCustomerDal : GenericRepository<Customer>, ICustomerDal
    {
        private readonly RentalyContext _context;

        public EfCustomerDal(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetCustomerByIdDto> GetByEmailAsync(string email)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Email == email);

            return new GetCustomerByIdDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                Phone = customer.Phone,
                IdentityNumber = customer.IdentityNumber,
                DrivingLicenseNumber = customer.DrivingLicenseNumber,
                DrivingLicenseDate = customer.DrivingLicenseDate
            };
        }
    }
}
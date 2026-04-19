using Rentaly.DtoLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.Abstract
{
    public interface ICustomerDal : IGenericDal<Customer>
    {
        Task<GetCustomerByIdDto> GetByEmailAsync(string email);
    }
}
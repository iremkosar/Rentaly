using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Concrete
{
    public class LocationManager : ILocationService
    {
        private readonly ILocationDal _locationDal;

        public LocationManager(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _locationDal.DeleteAsync(id);
        }

        public Task<Location> TGetByIdAsync(int id)
        {
            return _locationDal.GetByIdAsync(id);
        }

        public Task<List<Location>> TGetListAsync()
        {
            return _locationDal.GetListAsync();
        }

        public async Task TInsertAsync(Location entity)
        {
            await _locationDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Location entity)
        {
            await _locationDal.UpdateAsync(entity);
        }
    }
}

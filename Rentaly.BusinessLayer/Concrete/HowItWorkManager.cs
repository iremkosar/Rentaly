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
    public class HowItWorkManager : IHowItWorkService
    {
        private readonly IHowItWorkDal _howItWorkDal;

        public HowItWorkManager(IHowItWorkDal howItWorkDal)
        {
            _howItWorkDal = howItWorkDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _howItWorkDal.DeleteAsync(id);
        }

        public Task<HowItWork> TGetByIdAsync(int id)
        {
            return _howItWorkDal.GetByIdAsync(id);
        }

        public Task<List<HowItWork>> TGetListAsync()
        {
            return _howItWorkDal.GetListAsync();
        }

        public async Task TInsertAsync(HowItWork entity)
        {
            await _howItWorkDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(HowItWork entity)
        {
            await _howItWorkDal.UpdateAsync(entity);
        }
    }
}

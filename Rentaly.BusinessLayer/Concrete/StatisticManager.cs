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
    public class StatisticManager : IStatisticService
    {
        private readonly IStatisticDal _statisticDal;

        public StatisticManager(IStatisticDal statisticDal)
        {
            _statisticDal = statisticDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _statisticDal.DeleteAsync(id);
        }

        public async Task<Statistic> TGetByIdAsync(int id)
        {
            return await _statisticDal.GetByIdAsync(id);
        }

        public async Task<List<Statistic>> TGetListAsync()
        {
            return await _statisticDal.GetListAsync();
        }

        public async Task TInsertAsync(Statistic entity)
        {
            await _statisticDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Statistic entity)
        {
            await _statisticDal.UpdateAsync(entity);
        }
    }
}

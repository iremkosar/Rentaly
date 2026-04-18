using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Concrete
{
    public class AdventureManager : IAdventureService 
    {
        private readonly IAdventureDal _adventureDal;

        public AdventureManager(IAdventureDal adventureDal)
        {
            _adventureDal = adventureDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _adventureDal.DeleteAsync(id);
        }

        public async Task<Adventure> TGetByIdAsync(int id)
        {
            return await _adventureDal.GetByIdAsync(id);
        }

        public async Task<List<Adventure>> TGetListAsync()
        {
            return await _adventureDal.GetListAsync();
        }

        public async Task TInsertAsync(Adventure entity)
        {
            await _adventureDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Adventure entity)
        {
            await _adventureDal.UpdateAsync(entity);
        }
    }
}
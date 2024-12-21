using BonusServices.Services;
using Microsoft.EntityFrameworkCore;
using ModelDTO.Bonus;

namespace BonusServices.DataAcess
{
    public class PrivilegeDA : IPrivilegeDA
    {
        private readonly BonusDBContext bonusDB;

        public PrivilegeDA(BonusDBContext bonusDBcontext)
        {
            bonusDB = bonusDBcontext;
        }

        public async Task<List<Privilege>> FindAll(int page, int size)
        {
            try
            {
                var privileges = await bonusDB.privilege.OrderBy(x => x.id).Skip((page - 1) * size).Take(size).ToListAsync();
                return privileges;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Privilege> FindByID(int privilegeId)
        {
            try
            {
                var privilege = await bonusDB.privilege.FirstOrDefaultAsync(x => x.id == privilegeId);
                return privilege;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Privilege> FindByUsername(string username)
        {
            try
            {
                var privilege = await bonusDB.privilege.FirstOrDefaultAsync(x => x.username == username);
                return privilege;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Privilege> Add(Privilege privilege)
        {
            try
            {
                var id = bonusDB.privilege.Count() + 1;
                privilege.id = id;
                await bonusDB.privilege.AddAsync(privilege);
                await bonusDB.SaveChangesAsync();
                return privilege;
            }
            catch
            {
                throw;
            }
        }
        
        public async Task<Privilege> Update(Privilege privilege)
        {
            try
            {
                bonusDB.privilege.Update(privilege);
                await bonusDB.SaveChangesAsync();
                return privilege;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Privilege> Create(Privilege privilege)
        {
            try
            {
                bonusDB.privilege.Add(privilege);
                await bonusDB.SaveChangesAsync();
                return privilege;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> DeleteByID(int id)
        {
            try
            {
                Privilege privilege = await FindByID(id);
                bonusDB.privilege.Remove(privilege);
                await bonusDB.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return -1;
            }
        }
    }
}

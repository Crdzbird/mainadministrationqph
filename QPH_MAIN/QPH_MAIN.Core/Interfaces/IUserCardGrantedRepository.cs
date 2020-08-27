using QPH_MAIN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUserCardGrantedRepository : IRepository<UserCardGranted> {
        Task RemoveByUserId(int userId);
        Task<int> GetByCardAndUser(int idCard, int idUser);
    }
}
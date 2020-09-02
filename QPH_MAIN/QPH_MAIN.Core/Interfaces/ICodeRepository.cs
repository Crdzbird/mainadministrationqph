using QPH_MAIN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ICodeRepository<T> where T : BaseEntityCode
    {
        IEnumerable<T> GetAll();
        Task<T> GetByCode(string code);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(string code);
    }
}
using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly QPHContext _context;
        protected readonly DbSet<T> _entities;
        public BaseRepository(QPHContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll() => _entities.AsEnumerable();
        public async Task<T> GetById(int id) => await _entities.FindAsync(id);
        public async Task Add(T entity) => await _entities.AddAsync(entity);
        public void Update(T entity) => _entities.Update(entity);
        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
        }
    }
}
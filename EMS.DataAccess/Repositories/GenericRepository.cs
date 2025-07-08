using EMS.DataAccess.EMSDbContext;
using EMS.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EMXDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(EMXDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);
    }

}

using Lant_Hotelier.Data;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _context.Update(entity);

        public void Delete(T entity) =>
            _dbSet.Remove(entity);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}

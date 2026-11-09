using Lant_Hotelier.Data;
using Lant_Hotelier.Models;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Repositories
{
    public class EvenimenteRepository : GenericRepository<Evenimente>
    {
        public EvenimenteRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Evenimente>> GetByLantIdAsync(int lantId) =>
            await _dbSet
                .Include(e => e.LantHotelier)
                .Where(e => e.ID_Lant == lantId)
                .ToListAsync();
    }
}

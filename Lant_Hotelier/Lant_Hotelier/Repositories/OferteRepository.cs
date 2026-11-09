using Lant_Hotelier.Data;
using Lant_Hotelier.Models;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Repositories
{
    public class OferteRepository : GenericRepository<Oferte>
    {
        public OferteRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Oferte>> GetByLantIdAsync(int lantId) =>
            await _dbSet
                .Include(o => o.LantHotelier)
                .Where(o => o.ID_Lant == lantId)
                .ToListAsync();
    }
}

using Lant_Hotelier.Data;
using Lant_Hotelier.Models;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Repositories
{
    public class ServiciiRepository : GenericRepository<Servicii>
    {
        public ServiciiRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Servicii>> GetByLantIdAsync(int lantId) =>
            await _dbSet
                .Include(s => s.LantHotelier)
                .Where(s => s.ID_Lant == lantId)
                .ToListAsync();
    }
}

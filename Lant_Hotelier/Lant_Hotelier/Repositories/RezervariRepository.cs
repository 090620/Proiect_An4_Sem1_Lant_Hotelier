using Lant_Hotelier.Data;
using Lant_Hotelier.Models;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Repositories
{
    public class RezervariRepository : GenericRepository<Rezervare>
    {
        public RezervariRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Rezervare>> GetByLantIdAsync(int lantId) =>
            await _dbSet
                .Include(r => r.Camera).ThenInclude(c => c.LantHotelier)
                .Include(r => r.Hotel)
                .Include(r => r.Oferta)
                .Where(r => r.Camera.ID_Lant == lantId)
                .OrderByDescending(r => r.DataCreare)
                .ToListAsync();

        // Toate rezervarile (pentru Admin)
        public async Task<List<Rezervare>> GetAllWithDetailsAsync() =>
            await _dbSet
                .Include(r => r.Camera).ThenInclude(c => c.LantHotelier)
                .Include(r => r.Hotel)
                .Include(r => r.Oferta)
                .OrderByDescending(r => r.DataCreare)
                .ToListAsync();

        // Rezervarile unui user specific
        public async Task<List<Rezervare>> GetByUserIdAsync(string userId) =>
            await _dbSet
                .Include(r => r.Camera).ThenInclude(c => c.LantHotelier)
                .Include(r => r.Hotel)
                .Include(r => r.Oferta)
                .Where(r => r.AspNetUserId == userId)
                .OrderByDescending(r => r.DataCreare)
                .ToListAsync();

        public async Task<Rezervare?> GetWithDetailsAsync(int id) =>
            await _dbSet
                .Include(r => r.Camera).ThenInclude(c => c.LantHotelier)
                .Include(r => r.Hotel)
                .Include(r => r.Oferta)
                .FirstOrDefaultAsync(r => r.ID_Rezervare == id);
    }
}

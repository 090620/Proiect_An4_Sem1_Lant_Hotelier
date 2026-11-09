using Lant_Hotelier.Data;
using Lant_Hotelier.Models;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Repositories
{
    public class HoteluriRepository : GenericRepository<Hotel>
    {
        public HoteluriRepository(ApplicationDbContext context) : base(context) { }

        // Metoda specifica - filtreaza hoteluri dupa lant, cu Include
        public async Task<List<Hotel>> GetByLantIdAsync(int lantId) =>
            await _dbSet
                .Include(h => h.LantHotelier)
                .Where(h => h.ID_Lant == lantId)
                .ToListAsync();

        // Metoda specifica - ia numele lantului
        public async Task<string> GetNumeLantAsync(int lantId)
        {
            var lant = await _context.LanturiHoteliere.FindAsync(lantId);
            return lant?.Nume_Lant ?? "Lanț Necunoscut";
        }
    }
}
using Lant_Hotelier.Data;
using Lant_Hotelier.Models;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Repositories
{
    public class GalerieRepository : GenericRepository<Galerie>
    {
        public GalerieRepository(ApplicationDbContext context) : base(context) { }

        // Metoda specifica - GetAll cu Include-uri nested
        public async Task<List<Galerie>> GetAllWithDetailsAsync() =>
            await _dbSet
                .Include(g => g.Hotel)
                    .ThenInclude(h => h.LantHotelier)
                .ToListAsync();

        // Metoda specifica - GetById cu Include pentru Hotel
        public async Task<Galerie> GetByIdWithDetailsAsync(int id) =>
            await _dbSet
                .Include(g => g.Hotel)
                .FirstOrDefaultAsync(g => g.ID_Galerie == id);

        
        public new async Task<Galerie> GetByIdAsync(int id) =>
            await _dbSet
                .Include(g => g.Hotel)
                .FirstOrDefaultAsync(g => g.ID_Galerie == id);
    }
}
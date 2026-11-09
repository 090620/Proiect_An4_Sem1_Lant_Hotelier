using Lant_Hotelier.Data;
using Lant_Hotelier.Models;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Repositories
{
    public class RecenziiRepository : GenericRepository<Recenzie>
    {
        public RecenziiRepository(ApplicationDbContext context) : base(context) { }

        // Metoda specifica - filtreaza recenzii dupa lant, cu Include
        public async Task<List<Recenzie>> GetByLantIdAsync(int lantId) =>
            await _dbSet
                .Include(r => r.Hotel)
                .Where(r => r.Hotel.ID_Lant == lantId)
                .ToListAsync();
    }
}
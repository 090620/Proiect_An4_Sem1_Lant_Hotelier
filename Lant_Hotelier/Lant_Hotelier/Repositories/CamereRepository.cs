using Lant_Hotelier.Data;
using Lant_Hotelier.Models;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Repositories
{
    public class CamereRepository : GenericRepository<Camera>
    {
        public CamereRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Camera>> GetByLantIdAsync(int lantId) =>
            await _dbSet
                .Include(c => c.LantHotelier)
                .Where(c => c.ID_Lant == lantId)
                .ToListAsync();

        // Camere filtrate dupa lantul hotelului (camera e la nivel de lant)
        public async Task<List<Camera>> GetByHotelIdAsync(int hotelId)
        {
            // Hotel are ID_Lant, deci luam lantId din hotel
            var hotel = await _context.Hoteluri.FindAsync(hotelId);
            if (hotel == null) return new List<Camera>();
            return await GetByLantIdAsync(hotel.ID_Lant);
        }
    }
}

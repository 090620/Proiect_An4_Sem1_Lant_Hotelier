using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories; 

namespace Lant_Hotelier.Services
{
    public class HoteluriService
    {
        private readonly HoteluriRepository _repo;

        
        public HoteluriService(HoteluriRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Hotel>> GetHoteluriByLantIdAsync(int lantId)
        {
           
            return await _repo.GetByLantIdAsync(lantId);
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task CreateHotelAsync(Hotel hotel)
        {
            await _repo.AddAsync(hotel);
            await _repo.SaveAsync();
        }

        public async Task UpdateHotelAsync(Hotel hotel)
        {
            _repo.Update(hotel);
            await _repo.SaveAsync();
        }

        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await _repo.GetByIdAsync(id);
            if (hotel != null)
            {
                _repo.Delete(hotel);
                await _repo.SaveAsync();
            }
        }

        public async Task<string> GetNumeLantAsync(int lantId)
        {
            return await _repo.GetNumeLantAsync(lantId);
        }
    }
}
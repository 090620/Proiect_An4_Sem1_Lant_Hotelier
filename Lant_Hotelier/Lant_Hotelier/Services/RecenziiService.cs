using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories; // Asigură-te că ai acest using

namespace Lant_Hotelier.Services
{
    public class RecenziiService
    {
        private readonly RecenziiRepository _repo;

        public RecenziiService(RecenziiRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Recenzie>> GetRecenziiByLantIdAsync(int lantId)
        {
            return await _repo.GetByLantIdAsync(lantId);
        }

        public async Task CreateRecenzieAsync(Recenzie recenzie)
        {
            await _repo.AddAsync(recenzie);
            await _repo.SaveAsync();
        }

        public async Task DeleteRecenzieAsync(int id)
        {
            var recenzie = await _repo.GetByIdAsync(id);
            if (recenzie != null)
            {
                _repo.Delete(recenzie);
                await _repo.SaveAsync();
            }
        }
    }
}
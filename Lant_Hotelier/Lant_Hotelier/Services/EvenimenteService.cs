using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories;

namespace Lant_Hotelier.Services
{
    public class EvenimenteService
    {
        private readonly EvenimenteRepository _repo;

        public EvenimenteService(EvenimenteRepository repo) => _repo = repo;

        public async Task<List<Evenimente>> GetEvenimenteByLantIdAsync(int lantId) =>
            await _repo.GetByLantIdAsync(lantId);

        public async Task<Evenimente> GetEvenimentByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task CreateEvenimentAsync(Evenimente ev)
        {
            await _repo.AddAsync(ev);
            await _repo.SaveAsync();
        }

        public async Task UpdateEvenimentAsync(Evenimente ev)
        {
            _repo.Update(ev);
            await _repo.SaveAsync();
        }

        public async Task DeleteEvenimentAsync(int id)
        {
            var ev = await _repo.GetByIdAsync(id);
            if (ev != null) { _repo.Delete(ev); await _repo.SaveAsync(); }
        }
    }
}

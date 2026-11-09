using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories;

namespace Lant_Hotelier.Services
{
    public class ServiciiService
    {
        private readonly ServiciiRepository _repo;
        private readonly LanturiRepository _lanturiRepo;

        public ServiciiService(ServiciiRepository repo, LanturiRepository lanturiRepo)
        {
            _repo = repo;
            _lanturiRepo = lanturiRepo;
        }

        public async Task<List<Servicii>> GetServiciiByLantIdAsync(int lantId) =>
            await _repo.GetByLantIdAsync(lantId);

        public async Task<Servicii> GetServiciuByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task<List<LantHotelier>> GetToateLanturileAsync() =>
            await _lanturiRepo.GetAllAsync();

        public async Task CreateServiciuAsync(Servicii serviciu)
        {
            await _repo.AddAsync(serviciu);
            await _repo.SaveAsync();
        }

        public async Task UpdateServiciuAsync(Servicii serviciu)
        {
            _repo.Update(serviciu);
            await _repo.SaveAsync();
        }

        public async Task DeleteServiciuAsync(int id)
        {
            var serviciu = await _repo.GetByIdAsync(id);
            if (serviciu != null)
            {
                _repo.Delete(serviciu);
                await _repo.SaveAsync();
            }
        }
    }
}

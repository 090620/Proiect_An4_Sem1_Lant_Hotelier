using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories;

namespace Lant_Hotelier.Services
{
    public class OferteService
    {
        private readonly OferteRepository _repo;

        public OferteService(OferteRepository repo) => _repo = repo;

        public async Task<List<Oferte>> GetOferteByLantIdAsync(int lantId) =>
            await _repo.GetByLantIdAsync(lantId);

        public async Task<Oferte> GetOfertaByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task CreateOfertaAsync(Oferte oferta)
        {
            await _repo.AddAsync(oferta);
            await _repo.SaveAsync();
        }

        public async Task UpdateOfertaAsync(Oferte oferta)
        {
            _repo.Update(oferta);
            await _repo.SaveAsync();
        }

        public async Task DeleteOfertaAsync(int id)
        {
            var oferta = await _repo.GetByIdAsync(id);
            if (oferta != null)
            {
                _repo.Delete(oferta);
                await _repo.SaveAsync();
            }
        }
    }
}

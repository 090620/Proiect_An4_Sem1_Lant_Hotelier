using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories;

namespace Lant_Hotelier.Services
{
    public class GalerieService
    {
        private readonly GalerieRepository _galerieRepo;
        private readonly LanturiRepository _lantRepo;
        private readonly HoteluriRepository _hotelRepo;

        public GalerieService(GalerieRepository galerieRepo, LanturiRepository lantRepo, HoteluriRepository hotelRepo)
        {
            _galerieRepo = galerieRepo;
            _lantRepo = lantRepo;
            _hotelRepo = hotelRepo;
        }

        public async Task<List<Galerie>> GetToatePozeleAsync() => await _galerieRepo.GetAllWithDetailsAsync();
        public async Task<List<LantHotelier>> GetToateLanturileAsync() => await _lantRepo.GetAllAsync();
        public async Task<List<Hotel>> GetHoteluriByLantAsync(int lantId) => await _hotelRepo.GetByLantIdAsync(lantId);

        public async Task<Galerie> GetPozaByIdAsync(int id) => await _galerieRepo.GetByIdAsync(id);

        public async Task SalveazaPozaAsync(Galerie galerie) { await _galerieRepo.AddAsync(galerie); await _galerieRepo.SaveAsync(); }

        public async Task UpdatePozaAsync(Galerie galerie) { _galerieRepo.Update(galerie); await _galerieRepo.SaveAsync(); }

        public async Task DeletePozaAsync(int id)
        {
            var poza = await _galerieRepo.GetByIdAsync(id);
            if (poza != null) { _galerieRepo.Delete(poza); await _galerieRepo.SaveAsync(); }
        }
    }
}
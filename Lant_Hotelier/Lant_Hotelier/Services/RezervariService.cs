using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories;

namespace Lant_Hotelier.Services
{
    public class RezervariService
    {
        private readonly RezervariRepository _repo;
        private readonly CamereRepository _camereRepo;
        private readonly HoteluriRepository _hoteluriRepo;
        private readonly OferteRepository _oferteRepo;

        public RezervariService(RezervariRepository repo, CamereRepository camereRepo,
            HoteluriRepository hoteluriRepo, OferteRepository oferteRepo)
        {
            _repo = repo;
            _camereRepo = camereRepo;
            _hoteluriRepo = hoteluriRepo;
            _oferteRepo = oferteRepo;
        }

        public async Task<List<Rezervare>> GetRezervariByLantIdAsync(int lantId) =>
            await _repo.GetByLantIdAsync(lantId);

        public async Task<List<Rezervare>> GetAllAsync() =>
            await _repo.GetAllWithDetailsAsync();

        public async Task<List<Rezervare>> GetByUserIdAsync(string userId) =>
            await _repo.GetByUserIdAsync(userId);

        public async Task<Rezervare?> GetRezervareByIdAsync(int id) =>
            await _repo.GetWithDetailsAsync(id);

        public async Task<List<Camera>> GetCamereByHotelIdAsync(int hotelId) =>
            await _camereRepo.GetByHotelIdAsync(hotelId);

        public async Task<List<Camera>> GetCamereByLantIdAsync(int lantId) =>
            await _camereRepo.GetByLantIdAsync(lantId);

        public async Task<List<Hotel>> GetHoteluriByLantIdAsync(int lantId) =>
            await _hoteluriRepo.GetByLantIdAsync(lantId);

        public async Task<List<Oferte>> GetOferteByLantIdAsync(int lantId) =>
            await _oferteRepo.GetByLantIdAsync(lantId);

        public async Task<Camera?> GetCameraByIdAsync(int id) =>
            await _camereRepo.GetByIdAsync(id);

        public async Task CreateRezervareAsync(Rezervare rezervare)
        {
            await _repo.AddAsync(rezervare);
            await _repo.SaveAsync();
        }

        public async Task UpdateRezervareAsync(Rezervare rezervare)
        {
            _repo.Update(rezervare);
            await _repo.SaveAsync();
        }

        public async Task<bool> SchimbaStatusAsync(int id, StatusRezervare status)
        {
            var rezervare = await _repo.GetByIdAsync(id);
            if (rezervare == null) return false;
            rezervare.Status = status;
            _repo.Update(rezervare);
            await _repo.SaveAsync();
            return true;
        }

        public async Task DeleteRezervareAsync(int id)
        {
            var rezervare = await _repo.GetByIdAsync(id);
            if (rezervare != null) { _repo.Delete(rezervare); await _repo.SaveAsync(); }
        }
    }
}

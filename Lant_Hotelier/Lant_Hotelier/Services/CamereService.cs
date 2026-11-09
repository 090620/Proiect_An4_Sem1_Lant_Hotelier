using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories;

namespace Lant_Hotelier.Services
{
    public class CamereService
    {
        private readonly CamereRepository _repo;

        public CamereService(CamereRepository repo) => _repo = repo;

        public async Task<List<Camera>> GetCamereByLantIdAsync(int lantId) =>
            await _repo.GetByLantIdAsync(lantId);

        public async Task<Camera> GetCameraByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task CreateCameraAsync(Camera camera)
        {
            await _repo.AddAsync(camera);
            await _repo.SaveAsync();
        }

        public async Task UpdateCameraAsync(Camera camera)
        {
            _repo.Update(camera);
            await _repo.SaveAsync();
        }

        public async Task DeleteCameraAsync(int id)
        {
            var camera = await _repo.GetByIdAsync(id);
            if (camera != null)
            {
                _repo.Delete(camera);
                await _repo.SaveAsync();
            }
        }
    }
}

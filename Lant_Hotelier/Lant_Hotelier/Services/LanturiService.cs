using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories;

public class LanturiService
{
    private readonly LanturiRepository _repo;

    public LanturiService(LanturiRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<LantHotelier>> GetAllLanturiAsync() => await _repo.GetAllAsync();

    public async Task<LantHotelier> GetLantByIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task CreateLantAsync(LantHotelier lant)
    {
        await _repo.AddAsync(lant);
        await _repo.SaveAsync();
    }

    public async Task UpdateLantAsync(LantHotelier lant)
    {
        _repo.Update(lant);
        await _repo.SaveAsync();
    }

    public async Task DeleteLantAsync(int id)
    {
        var lant = await _repo.GetByIdAsync(id);
        if (lant != null)
        {
            _repo.Delete(lant);
            await _repo.SaveAsync();
        }
    }
}
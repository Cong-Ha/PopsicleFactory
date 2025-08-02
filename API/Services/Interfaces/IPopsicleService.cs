using API.Models;

namespace API.Services.Interfaces;

public interface IPopsicleService
{
    Task<PopsicleViewModel?> GetPopsicleAsync(int id);
    Task<PopsicleViewModel> CreatePopsicleAsync(PopsicleCreateModel model);
}
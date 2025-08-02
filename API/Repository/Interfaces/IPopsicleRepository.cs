using API.Models;

namespace API.Repository.Interfaces;

public interface IPopsicleRepository
{
    Task<Popsicle?> GetPopsicleAsync(int id);
    Task<Popsicle> CreatePopsicleAsync(Popsicle entity);
}
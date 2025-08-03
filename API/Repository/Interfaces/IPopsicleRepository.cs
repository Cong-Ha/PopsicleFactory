using API.Models;

namespace API.Repository.Interfaces;

public interface IPopsicleRepository
{
    Task<Popsicle?> GetPopsicleAsync(int id);
    Task<Popsicle> CreatePopsicleAsync(PopsicleCreateModel model);
    Task<Popsicle> ReplacePopsicleAsync(PopsicleReplaceModel model, Popsicle entity);
    Task DeletePopsicleAsync(Popsicle entity);
    Task<Popsicle> UpdatePopsicleAsync(PopsicleUpdateModel model,  Popsicle entity);
    Task<IEnumerable<Popsicle>> GetPopsiclesAsync(PopsicleSearchModel model);
}
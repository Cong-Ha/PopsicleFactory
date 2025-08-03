using API.Models;

namespace API.Services.Interfaces;

public interface IPopsicleService
{
    Task<PopsicleViewModel> GetPopsicleAsync(int id);
    Task<PopsicleViewModel> CreatePopsicleAsync(PopsicleCreateModel model);
    Task<PopsicleViewModel> ReplacePopsicle(int id, PopsicleReplaceModel model);
    Task DeletePopsicleAsync(int id);
    Task<PopsicleViewModel> UpdatePopsicleAsync(int id, PopsicleUpdateModel model);
    Task<List<PopsicleViewModel>> SearchPopsicleAsync(PopsicleSearchModel query);
}
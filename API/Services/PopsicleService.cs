using API.Models;
using API.Repository.Interfaces;
using API.Services.Interfaces;

namespace API.Services;

public class PopsicleService(IPopsicleRepository repository) : IPopsicleService
{
    public async Task<PopsicleViewModel?> GetPopsicleAsync(int id)
    {
        var popsicle = await repository.GetPopsicleAsync(id);
        if (popsicle == null)
        {
            return null;
        }
        var popsicleViewModel = new PopsicleViewModel
        {
            Id = popsicle.Id,
            Name = popsicle.Name,
            Type = popsicle.Type,
            Size = popsicle.Size,
            Description = popsicle.Description,
            IsOrganic = popsicle.IsOrganic,
            IsSugarFree = popsicle.IsSugarFree,
            Price = popsicle.Price
        };
        
        return popsicleViewModel;
    }

    public async Task<PopsicleViewModel> CreatePopsicleAsync(PopsicleCreateModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "PopsicleCreateModel cannot be null.");
        }
        
        //adjust price
        var price = Math.Round(model.Price, 2, MidpointRounding.AwayFromZero);
        
        // map model to entity
        var entity = new Popsicle
        {
            Name = model.Name,
            Type = model.Type,
            Size = model.Size,
            Description = model.Description,
            IsOrganic = model.IsOrganic,
            IsSugarFree = model.IsSugarFree,
            Price = price
        };
        //then call repo to save
        await repository.CreatePopsicleAsync(entity);
        
        //map entity to view model
        return new PopsicleViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = entity.Type,
            Size = entity.Size,
            Description = entity.Description,
            IsOrganic = entity.IsOrganic,
            IsSugarFree = entity.IsSugarFree,
            Price = entity.Price
        };
    }
}
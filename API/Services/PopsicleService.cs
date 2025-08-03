using API.Models;
using API.Repository.Interfaces;
using API.Services.Interfaces;

namespace API.Services;

public class PopsicleService(IPopsicleRepository repository) : IPopsicleService
{
    public async Task<PopsicleViewModel> GetPopsicleAsync(int id)
    {
        Popsicle? popsicle = await repository.GetPopsicleAsync(id);

        if (popsicle == null)
        {
            throw new KeyNotFoundException($"Popsicle with id {id} was not found");
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
        model.Price = Math.Round(model.Price, 2, MidpointRounding.AwayFromZero);
        
        //then call repo to save
        Popsicle entity = await repository.CreatePopsicleAsync(model);
        
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

    private async Task<Popsicle?> GetPopsicleByIdAsync(int id)
    {
        var popsicle = await repository.GetPopsicleAsync(id);
        return popsicle;
    }

    public async Task<PopsicleViewModel> ReplacePopsicle(int id, PopsicleReplaceModel model)
    {
        Popsicle? popsicle = await GetPopsicleByIdAsync(id);

        if (popsicle == null)
        {
            throw new KeyNotFoundException($"No popsicle found with id {id}");
        }
        //update props
        var updatedPopsicle = await repository.ReplacePopsicleAsync(model, popsicle);
        
        //return view model with updated props
        return new PopsicleViewModel
        {
            Id = updatedPopsicle.Id,
            Name = updatedPopsicle.Name,
            Type = updatedPopsicle.Type,
            Size = updatedPopsicle.Size,
            Description = updatedPopsicle.Description,
            IsOrganic = updatedPopsicle.IsOrganic,
            IsSugarFree = updatedPopsicle.IsSugarFree,
            Price = updatedPopsicle.Price
        };
    }
    
    public async Task DeletePopsicleAsync(int id)
    {
        Popsicle? popsicle = await GetPopsicleByIdAsync(id);

        if (popsicle == null)
        {
            throw new KeyNotFoundException($"No popsicle found with id {id}");
        }
        await repository.DeletePopsicleAsync(popsicle);
    }

    private bool IsPatchEmpty(PopsicleUpdateModel model)
    {
        return model.Name == null &&
               model.Type == null &&
               model.Size == null &&
               model.Description == null &&
               model.IsOrganic == null &&
               model.IsSugarFree == null &&
               model.Price == null;

    }

    public async Task<PopsicleViewModel> UpdatePopsicleAsync(int id, PopsicleUpdateModel model)
    {
        Popsicle? popsicle = await GetPopsicleByIdAsync(id);
        if (popsicle == null)
        {
            throw new KeyNotFoundException($"No popsicle found with id {id}");
        }
        
        //validate request body
        if (IsPatchEmpty(model))
        {
            throw new ArgumentException("At least one property for popsicle must be provided for update.");
        }
        
        var updatedPopsicle = await repository.UpdatePopsicleAsync(model, popsicle);

        return new PopsicleViewModel
        {
            Id = updatedPopsicle.Id,
            Name = updatedPopsicle.Name,
            Type = updatedPopsicle.Type,
            Size = updatedPopsicle.Size,
            Description = updatedPopsicle.Description,
            IsOrganic = updatedPopsicle.IsOrganic,
            IsSugarFree = updatedPopsicle.IsSugarFree,
            Price = updatedPopsicle.Price
        };
    }

    private bool IsSearchEmpty(PopsicleSearchModel model)
    {
        return model.Name == null &&
               model.Type == null &&
               model.Size == null &&
               model.IsSugarFree == null &&
               model.IsOrganic == null &&
               model.MinPrice == null &&
               model.MaxPrice == null;
    }

    public async Task<List<PopsicleViewModel>> SearchPopsicleAsync(PopsicleSearchModel query)
    {
        if (IsSearchEmpty(query))
        {
            throw new ArgumentException("At least one property for popsicle must be provided for search.");
        }
        
        //query data
        IEnumerable<Popsicle> popsicles = await repository.GetPopsiclesAsync(query);
        
        //map data
        return popsicles.Select(p => new PopsicleViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Type = p.Type,
            Size = p.Size,
            IsSugarFree = p.IsSugarFree,
            IsOrganic = p.IsOrganic,
            Price = p.Price
        }).ToList();
    }
}
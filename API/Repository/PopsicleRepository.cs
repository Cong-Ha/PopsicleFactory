using API.Data;
using API.Models;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class PopsicleRepository(PopsicleFactoryContext context) : IPopsicleRepository
{
    public async Task<Popsicle?> GetPopsicleAsync(int id)
    {
        return await context.Popsicles.FindAsync(id);
    }

    public async Task<Popsicle> CreatePopsicleAsync(PopsicleCreateModel model)
    {
        // map model to entity
        var entity = new Popsicle
        {
            Name = model.Name,
            Type = model.Type,
            Size = model.Size,
            Description = model.Description,
            IsOrganic = model.IsOrganic,
            IsSugarFree = model.IsSugarFree,
            Price = model.Price
        };
        
        context.Popsicles.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Popsicle> ReplacePopsicleAsync(PopsicleReplaceModel model, Popsicle entity)
    {
        entity.Name = model.Name;
        entity.Type = model.Type;
        entity.Size = model.Size;
        entity.Description = model.Description;
        entity.IsSugarFree = model.IsSugarFree;
        entity.IsOrganic = model.IsOrganic;
        entity.Price = model.Price;
        
        context.Popsicles.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task DeletePopsicleAsync(Popsicle entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Popsicle to delete cannot be null.");
        }

        context.Popsicles.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<Popsicle> UpdatePopsicleAsync(PopsicleUpdateModel model,  Popsicle entity)
    {
        if(model.Name != null) entity.Name = model.Name;
        if(model.Type.HasValue) entity.Type = (PopsicleType)model.Type;
        if(model.Size.HasValue) entity.Size = (PopsicleSize)model.Size;
        if (model.Description != null) entity.Description = model.Description;
        if (model.IsOrganic.HasValue) entity.IsOrganic = (bool)model.IsOrganic;
        if(model.IsSugarFree.HasValue) entity.IsSugarFree = (bool)model.IsSugarFree;
        if (model.Price.HasValue) entity.Price = (decimal)model.Price;
        
        context.Popsicles.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Popsicle>> GetPopsiclesAsync(PopsicleSearchModel model)
    {
        IQueryable<Popsicle> query = context.Popsicles.AsQueryable();

        if (!string.IsNullOrEmpty(model.Name))
        {
            query = query.Where(p => p.Name.Contains(model.Name));
        }
        
        if (model.Type.HasValue)
        {
            query = query.Where(p => p.Type == model.Type);
        }

        if (model.Size.HasValue)
        {
            query = query.Where(p => p.Size == model.Size);
        }

        if (model.IsOrganic.HasValue)
        {
            query = query.Where(p => p.IsOrganic == model.IsOrganic);
        }

        if (model.IsSugarFree.HasValue)
        {
            query = query.Where(p => p.IsSugarFree == model.IsSugarFree);
        }

        if (model is { MinPrice: not null, MaxPrice: not null })
        {
            query = query.Where(p => p.Price >= model.MinPrice && p.Price <= model.MaxPrice);
        }
        return await query.ToListAsync();
    }
}
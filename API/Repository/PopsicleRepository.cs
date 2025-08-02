using API.Data;
using API.Models;
using API.Repository.Interfaces;

namespace API.Repository;

public class PopsicleRepository(PopsicleFactoryContext context) : IPopsicleRepository
{
    public async Task<Popsicle?> GetPopsicleAsync(int id)
    {
        return await context.Popsicles.FindAsync(id);
    }

    public async Task<Popsicle> CreatePopsicleAsync(Popsicle entity)
    {
        context.Popsicles.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}
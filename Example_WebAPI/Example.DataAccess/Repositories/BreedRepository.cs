using Example.DataAccess.Models;
using Example.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Example.DataAccess.Repositories;

public class BreedRepository : IBreedRepository
{
    private readonly ExampleContext _context;

    public BreedRepository() { }

    public BreedRepository(ExampleContext context)
    {
        _context = context;
    }

    public async Task Create(Breed item)
    {
        await _context.Breeds.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var item = GetBy(b => b.Id == id);

        if (item != null)
        {
            _context.ChangeTracker.Clear();
            _context.Breeds.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Update(Breed item)
    {
        // https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker.clear?view=efcore-5.0
        _context.ChangeTracker.Clear();
        
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public IEnumerable<Breed> Get()
        => _context.Breeds.ToList();

    public IEnumerable<Breed> Get(Func<Breed, bool> predicate)
        => _context.Breeds.Where(predicate).ToList();

    public Breed? GetBy(Func<Breed, bool> predicate)
        => _context.Breeds.Where(predicate).FirstOrDefault();
}
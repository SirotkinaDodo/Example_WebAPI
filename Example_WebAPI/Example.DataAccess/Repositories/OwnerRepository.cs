using Example.DataAccess.Models;
using Example.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Example.DataAccess.Repositories;

public class OwnerRepository(ExampleContext context) : IOwnerRepository
{
    private readonly ExampleContext _context = context;

    public async Task Create(Owner item)
    {
        await _context.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Owner item)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Owner> Get()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Owner> Get(Func<Owner, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public Owner? GetBy(Func<Owner, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Owner> GetFull()
    {
        var dogs = context.Dogs;
        var ownersDogs = context.OwnersDogs;
        var wer = context.Owners
            .ToList();
        return context.Owners
            .ToList();
    }
}
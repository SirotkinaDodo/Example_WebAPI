using Example.DataAccess.Models;

namespace Example.DataAccess.Repositories.Contracts;

public interface IOwnerRepository : ICRUDRepository<Owner>, IReadRepository<Owner>
{
    IEnumerable<Owner> GetFull();
}
using Example.DataAccess.Models;

namespace Example.DataAccess.Repositories.Contracts;

public interface IBreedRepository : ICRUDRepository<Breed>, IReadRepository<Breed>
{
}
namespace Example.DataAccess.Repositories.Contracts;

public interface ICRUDRepository<TEntity> where TEntity : class
{
    Task Create(TEntity item);
    
    Task Delete(Guid id);
    
    Task Update(TEntity item);
}
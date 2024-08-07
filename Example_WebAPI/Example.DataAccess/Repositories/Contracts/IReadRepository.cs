namespace Example.DataAccess.Repositories.Contracts;

public interface IReadRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> Get();
    
    IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    
    TEntity? GetBy(Func<TEntity, bool> predicate);
}
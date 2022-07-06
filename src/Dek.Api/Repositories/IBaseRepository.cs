namespace Dek.Api.Repositories;

public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
{
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetById(Guid id);

    TEntity Create(TEntity entity);

    TEntity Update(TEntity entity);

    Task Delete(Guid id);

    Task<int> SaveChangesAsync();
}
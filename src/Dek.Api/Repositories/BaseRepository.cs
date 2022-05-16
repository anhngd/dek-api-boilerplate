using Dek.Api.Contexts;
using Dek.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dek.Api.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public BaseRepository(ApplicationDbContext dbContext)
    {
        Db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbSet = Db.Set<TEntity>();
    }

    private ApplicationDbContext Db { get; }

    private DbSet<TEntity> DbSet { get; }

    public virtual IQueryable<TEntity> GetAll()
    {
        return DbSet.AsNoTracking();
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual TEntity Create(TEntity entity)
    {
        DbSet.Add(entity);
        return entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        DbSet.Update(entity);
        return entity;
    }

    public virtual async Task Delete(Guid id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity != null) DbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Db.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    protected virtual void Dispose(bool disposing)
    {
        if (disposing) Db.Dispose();
    }
}
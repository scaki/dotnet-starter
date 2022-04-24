using CatalogService.Domain.Common;

namespace CatalogService.Application.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    IEnumerable<TEntity> Get();
    IEnumerable<TEntity> GetAsNoTracking();
    Task<IEnumerable<TEntity>> GetAsync();
    Task<IEnumerable<TEntity>> GetAsyncAsNoTracking();
    TEntity GetById(object id);
    TEntity GetByIdAsNoTracking(object id);
    Task<TEntity> GetByIdAsync(object id);
    Task<TEntity> GetByIdAsyncAsNoTracking(object id);
    void Insert(TEntity entity);
    void InsertAsync(TEntity entity);
    void InsertRange(IEnumerable<TEntity> entities);
    void InsertRangeAsync(IEnumerable<TEntity> entities);
    void Delete(object id);
    void DeleteRange(IEnumerable<TEntity> entitiesToDelete);
    void Update(TEntity entityToUpdate);
    void UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
    void Save();
    void SaveAsync();
}
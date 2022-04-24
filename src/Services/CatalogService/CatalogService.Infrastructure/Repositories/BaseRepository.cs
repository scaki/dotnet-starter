using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Common;
using CatalogService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public readonly ApplicationDbContext Context;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(ApplicationDbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<TEntity> GetAsNoTracking()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsyncAsNoTracking(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity GetByIdAsNoTracking(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return null;
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetByIdAsyncAsNoTracking(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return null;
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void InsertAsync(TEntity entity)
        {
            _dbSet.AddAsync(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRangeAsync(entities);
        }
        
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            var baseEntities = entitiesToDelete.ToList();
            foreach (var entity in baseEntities.Where(entity => Context.Entry(entity).State == EntityState.Detached))
            {
                _dbSet.Attach(entity);
            }

            _dbSet.RemoveRange(baseEntities);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            foreach (var entity in entitiesToUpdate)
            {
                _dbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public virtual void SaveAsync()
        {
            Context.SaveChangesAsync();
        }
    }
}
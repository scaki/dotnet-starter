using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Common;
using CatalogService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
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

        public virtual async Task<IEnumerable<TEntity>> GetAsyncAsNoTracking()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity GetByIdAsNoTracking(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return null;
            _context.Entry(entity).State = EntityState.Detached;
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
            _context.Entry(entity).State = EntityState.Detached;
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

        public virtual void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        protected virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            var baseEntities = entitiesToDelete.ToList();
            foreach (var entity in baseEntities.Where(entity => _context.Entry(entity).State == EntityState.Detached))
            {
                _dbSet.Attach(entity);
            }

            _dbSet.RemoveRange(baseEntities);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            foreach (var entity in entitiesToUpdate)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual void SaveAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}
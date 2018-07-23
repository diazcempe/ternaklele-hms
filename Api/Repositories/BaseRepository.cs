using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected DbContext Db; // use the generic dbcontext to support multiple contexts base repo.
        protected DbSet<TEntity> DbSet;
        protected IQueryable<TEntity> InternalQuery;

        public BaseRepository(DbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
            InternalQuery = DbSet;
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void AddRange(IEnumerable<TEntity> objList)
        {
            DbSet.AddRange(objList);
        }

        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return InternalQuery.ToList();
        }

        public DbSet<TEntity> All => DbSet;

        public virtual void Remove(object id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void RemoveAll()
        {
            DbSet.RemoveRange(DbSet);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return InternalQuery.AsNoTracking().Where(predicate);
        }

        public virtual async Task<TEntity> FindAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await InternalQuery.ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }

        public void Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            InternalQuery = InternalQuery.Include(navigationPropertyPath);
        }

        public IRepository<TEntity> IncludeChain<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            Include(navigationPropertyPath);
            return this;
        }

        public void UpdateOrAdd(TEntity updatedEntity, object entityId)
        {
            var entity = GetById(entityId);
            if (entity != null)
            {
                var attachedEntry = Db.Entry(entity);
                attachedEntry.CurrentValues.SetValues(updatedEntity);
            }
            else
            {
                DbSet.Add(updatedEntity);
            }
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

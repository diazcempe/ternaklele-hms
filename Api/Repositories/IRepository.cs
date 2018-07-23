using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void AddRange(IEnumerable<TEntity> objList);

        /// <summary>
        /// Updates an EXISTING entity to the updated entity OR add them to the context
        /// </summary>
        /// <param name="updatedEntity"></param>
        /// <param name="entityId"></param>
        void UpdateOrAdd(TEntity updatedEntity, object entityId);

        /// <summary>
        /// Fetch an entity with all of its navigation properties. Could be overridden to ensure more efficient fetching (obey the Include laws, if necessary).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);

        IEnumerable<TEntity> GetAll();

        DbSet<TEntity> All { get; }

        void Remove(object id);

        void RemoveAll();

        void Remove(TEntity entity);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds an entity inside the context store or fetch from DB asynchronously.
        /// Note that this WILL NOT obey the Include rules or fetch ANY Navigation props
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(object id);

        /// <summary>
        /// Fetch an entity with all of its navigation properties asynchronously.  Could be overridden to ensure more efficient fetching (obey the Include laws, if necessary).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<int> SaveChangesAsync();

        void Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath);

        IRepository<TEntity> IncludeChain<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath);
    }
}

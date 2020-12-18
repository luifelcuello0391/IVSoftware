using IVSoftware.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IVSoftware.Data.Service
{
    public interface IEntityService<TEntity, TKey>
        where TEntity : BaseModel<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Create record
        /// </summary>
        /// <param name="entity">Record to create</param>
        /// <returns></returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="entity">Record to update</param>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="entity">Record to delete</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Get record by id
        /// </summary>
        /// <param name="id">record id</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Get record by id
        /// and include relations
        /// </summary>
        /// <param name="id">record id</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAndIncludeAsync(TKey id,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get records that match
        /// some criteria
        /// </summary>
        /// <param name="expression">Condition to filter records</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Get records that match
        /// some criteria with relations
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindByConditionAndIncludeAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
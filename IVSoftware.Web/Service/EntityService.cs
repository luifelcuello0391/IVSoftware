using IVSoftware.Data.Models.Core;
using IVSoftware.Web.Models;
using IVSoftware.Web.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IVSoftware.Web.Service
{
    public class EntityService<TEntity, TKey> : EntityRepository<TEntity, TKey>, IEntityService<TEntity, TKey>
        where TEntity : BaseModel<TKey>
        where TKey : struct
    {
        private readonly IVSoftwareContext _context;

        public EntityService(IVSoftwareContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            Create(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await FindByCondition(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await FindByCondition(t => t.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAndIncludeAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await FindByConditionAndInclude(expression, includeProperties).ToListAsync();
        }

        public async Task<TEntity> GetByIdAndIncludeAsync(TKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await FindByConditionAndInclude(t => t.Id.Equals(id), includeProperties).FirstOrDefaultAsync();
        }
    }
}
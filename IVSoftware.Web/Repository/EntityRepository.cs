using IVSoftware.Data.Models.Core;
using IVSoftware.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IVSoftware.Web.Repository
{
    public abstract class EntityRepository<TEntity, TKey> : IEntityRepository<TEntity>
        where TEntity : BaseModel<TKey>
        where TKey : struct
    {
        protected IVSoftwareContext RepositoryContext { get; set; }

        public EntityRepository(IVSoftwareContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void Create(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> FindAll()
        {
            return RepositoryContext.Set<TEntity>();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return RepositoryContext.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> FindByConditionAndInclude(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = RepositoryContext.Set<TEntity>().Where(expression);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public void Update(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Update(entity);
        }
    }
}
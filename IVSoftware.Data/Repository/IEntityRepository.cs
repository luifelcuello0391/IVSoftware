using System;
using System.Linq;
using System.Linq.Expressions;

namespace IVSoftware.Data.Repository
{
    public interface IEntityRepository<TEntity>
    {
        IQueryable<TEntity> FindAll();

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> FindByConditionAndInclude(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includeProperties);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ZnymkyHub.DAL.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class //обмеження, де TEntity є тип class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll(); //повертає колекцію
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}

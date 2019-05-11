using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(); //повертає колекцію
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}

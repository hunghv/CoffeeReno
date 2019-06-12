using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entities.Base;

namespace Data.Repository.Base
{
    public interface IEntityBaseRepository<T> where T : Entity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllNoneDeleted();
        int Count();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleNoneDeletedAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        T GetSingleNoneDeleted(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        bool Commit();
        Task<bool> CommitAsync();
    }
}

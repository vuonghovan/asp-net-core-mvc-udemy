using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Factories.Services
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> GetById(long Id);
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void AddEntity(T entity);
        void AddEntities(IEnumerable<T> entities);
        void UpdateEntity(T entity);
        void RemoveEntityById(long Id);
        void RemoveEntity(T entity);
        void RemoveEntities(IEnumerable<T> entities);
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        Task<int> SaveAll();
        bool DiscardAllChange();
    }
}

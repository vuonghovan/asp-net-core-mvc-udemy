using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models;

namespace Factories.Services
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/ef/ef6/saving/change-tracking/entity-state
    /// Added: the entity is being tracked by the context but does not yet exist in the database
    /// Unchanged: the entity is being tracked by the context and exists in the database, and its property values have not changed from the values in the database
    /// Modified: the entity is being tracked by the context and exists in the database, and some or all of its property values have been modified
    /// Deleted: the entity is being tracked by the context and exists in the database, but has been marked for deletion from the database the next time SaveChanges is called
    /// Detached: the entity is not being tracked by the context
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        internal DbSet<T> _dbSet;
        internal MyDbContext _dbContext;
        internal IDbContextTransaction _transaction;

        /// <summary>
        /// Tracking vs. No-Tracking Queries
        /// https://docs.microsoft.com/en-us/ef/core/querying/tracking
        /// </summary>
        /// <param name="context"></param>
        public Repository(MyDbContext context)
        {
            this._dbContext = context;
            this._dbSet = context.Set<T>();
        }

        public async Task<T> GetById(long Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async void AddEntity(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async void AddEntities(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void UpdateEntity(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async void RemoveEntityById(long Id)
        {
            T entity = await _dbSet.FindAsync(Id);
            if (entity == null)
                throw new Exception($"Not found {nameof(T)} by Id = {Id}");
            RemoveEntity(entity);
        }

        /// <summary>
        /// Unchanged entities are not touched by SaveChanges. Updates are not sent to the database for entities in the Unchanged state.
        /// Added entities are inserted into the database and then become Unchanged when SaveChanges returns.
        /// Modified entities are updated in the database and then become Unchanged when SaveChanges returns.
        /// Deleted entities are deleted from the database and are then detached from the context.
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<int> SaveAll()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public bool DiscardAllChange()
        {
            throw new NotImplementedException();
        }

        public IDbContextTransaction BeginTransaction()
        {
            try
            {
                return _transaction ?? (_transaction = _dbContext.Database.CurrentTransaction ??
                                                       _dbContext.Database.BeginTransaction());
            }
            catch
            {
                throw new Exception($"{nameof(T)} cannot begin transaction");
            }
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
                throw new Exception($"{nameof(T)} transaction is null");
            if (_transaction != _dbContext.Database.CurrentTransaction)
                throw new Exception($"{nameof(T)} current transaction invalid");

            _transaction.Commit();
        }

        public void RollBackTransaction()
        {
            if (_transaction == null)
                throw new Exception($"{nameof(T)} transaction is null");
            if (_transaction != _dbContext.Database.CurrentTransaction)
                throw new Exception($"{nameof(T)} current transaction invalid");

            _transaction.Rollback();
        }
    }
}

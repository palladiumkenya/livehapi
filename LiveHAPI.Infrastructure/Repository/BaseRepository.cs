using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public abstract class BaseRepository<T, TId> : IRepository<T, TId> where T : Entity<TId>, new()
    {

        internal LiveHAPIContext Context;
        internal DbSet<T> DbSet;

        protected BaseRepository(LiveHAPIContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }


        public T Get(TId id, bool voided = false)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll(bool voided = false)
        {
            return DbSet;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, bool voided = false)
        {
            return DbSet.Where(predicate);
        }

        public void Save(T entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public void InsertOrUpdate(T entity)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdateAny(object entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }
    

        public void Delete(TId id)
        {
            throw new NotImplementedException();
        }

        public void Void(TId id)
        {
            throw new NotImplementedException();
        }
    }
}
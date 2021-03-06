﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Dapper;

namespace LiveHAPI.Infrastructure.Repository
{
    public abstract class BaseRepository<T, TId> : IRepository<T, TId> where T : Entity<TId>, new()
    {

        internal LiveHAPIContext Context;
        internal DbSet<T> DbSet;
        private SqlConnection _connection;

        protected BaseRepository(LiveHAPIContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }


        public virtual T Get(TId id, bool voided = false)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll(bool voided = false)
        {
            return DbSet;
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, bool voided = false)
        {
            return DbSet.Where(predicate);
        }

        public IEnumerable<TC> ExecQuery<TC>(string selectStatement)
        {
            var entities = GetDbConnection().Query<TC>(selectStatement);
            return entities;
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void InsertOrUpdate(T entity)
        {
            var exisits = Get(entity.Id);
            if (null != exisits)
            {
                Update(entity);
            }
            else
            {
                Insert(entity);
            }
        }

        public virtual void InsertOrUpdate(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                var exisits = Get(entity.Id);
                if (null != exisits)
                {
                    DbSet.Attach(entity);
                    Context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    DbSet.Add(entity);
                }
            }
        }

        public virtual void InsertOrUpdateAny(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TId id)
        {
            var entity = Get(id);
            if (null != entity)
                Context.Remove(entity);
        }

        public virtual void Void(TId id)
        {
            throw new NotImplementedException();
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public IDbConnection GetDbConnection(bool open = true)
        {
           // if (null == _connection)
                _connection = new SqlConnection(Context.Database.GetDbConnection().ConnectionString);

            if (open)
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                return _connection;
            }

            return _connection;
        }

        public void CloseDbConnection()
        {
            _connection?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeContext();
                DisposeConnection();
            }
        }
        private void DisposeContext()
        {
            Context?.Dispose();
            Context = null;
        }

        private void DisposeConnection()
        {
            _connection?.Dispose();
            _connection = null;
        }
    }
}

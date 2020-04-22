using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradingEngine.Api.Model.GeneratedContext;

namespace TradingEngine.Api.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TradingEngineContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(TradingEngineContext context)
        {
            if (context == null)
                throw new ArgumentNullException("Null DbContext");
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IQueryable<T>> GetAll()
        {
            return await Task.FromResult(_dbSet);
        }

        public virtual async Task<IQueryable<T>> GetMany(Expression<Func<T, bool>> where)
        {
            return await Task.FromResult(_dbSet.AsExpandable().Where(where));
        }

        public virtual IQueryable<T> GetBySP(string sp, params object[] parameters)
        {
            return null; //_context.Database.ex .SqlQuery<T>(sp, parameters).AsQueryable();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> GetById(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> where)
        {
           return await _dbSet.Where(where).FirstOrDefaultAsync();
        }

        public virtual int ExecBySP(string sp, params object[] parameters)
        {
            return 0;// _context.Database.ExecuteSqlCommand(sp, parameters);
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            //if (entity == null) return; // not found; assume already deleted.
            //Delete(entity);
        }

        public virtual void Delete(string id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            //Delete(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> entities = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T entity in entities)
                Delete(entity);
        }

        public virtual bool IsExist(Expression<Func<T, bool>> where)
        {
            var entity = _dbSet.Where(where);
            return entity.Count<T>() > 0;
        }
    }
}

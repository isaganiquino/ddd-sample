using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TradingEngine.Api.Implementations
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> GetBySP(string sp, params object[] parameters);
        Task<T> GetById(int id);
        Task<T> GetById(string id);
        Task<T> Get(Expression<Func<T, bool>> where);
        int ExecBySP(string sp, params object[] parameters);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Delete(string id);
        void Delete(Expression<Func<T, bool>> where);
        bool IsExist(Expression<Func<T, bool>> where);
    }
}

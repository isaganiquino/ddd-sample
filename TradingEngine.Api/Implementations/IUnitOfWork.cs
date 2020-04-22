using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingEngine.Api.Implementations
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
        void Dispose();
        IRepository<T> RepositoryFor<T>() where T : class;
    }
}

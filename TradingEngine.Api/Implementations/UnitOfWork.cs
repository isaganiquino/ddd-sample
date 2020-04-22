using System;
using System.Threading.Tasks;
using TradingEngine.Api.Model.GeneratedContext;

namespace TradingEngine.Api.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private TradingEngineContext _context;

        public UnitOfWork()
        {
            CreateDbContext();
        }

        //Repositories
        public IRepository<T> RepositoryFor<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        //Commit Changes to database
        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        protected void CreateDbContext()
        {
            _context = new TradingEngineContext();
            //context.Configuration.ValidateOnSaveEnabled = false;
        }

        //IDisposable
        #region IDisposable  

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

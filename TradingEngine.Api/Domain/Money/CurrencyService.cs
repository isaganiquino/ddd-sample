using System.Collections.Generic;
using System.Threading.Tasks;
using TradingEngine.Api.Implementations;
using TradingEngine.Api.Model.GeneratedContext;

namespace TradingEngine.Api.Domain.Trading
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync()
        {
            return await _unitOfWork.RepositoryFor<Currency>().GetAll();
        }

        public async Task<Currency> GetCurrency(int currencyId)
        {
            return await _unitOfWork.RepositoryFor<Currency>().GetById(currencyId);
        }

    }
}

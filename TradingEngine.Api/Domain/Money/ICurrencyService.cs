using System.Collections.Generic;
using System.Threading.Tasks;
using TradingEngine.Api.Model.GeneratedContext;

namespace TradingEngine.Api.Domain.Trading
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetAllCurrenciesAsync();
        Task<Currency> GetCurrency(int currencyId);
    }
}

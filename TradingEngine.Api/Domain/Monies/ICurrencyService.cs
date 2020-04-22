using System.Collections.Generic;
using System.Threading.Tasks;
using TradingEngine.Api.Model.GeneratedContext;

namespace TradingEngine.Api.Domain.Monies
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetAllCurrenciesAsync();
        Task<Currency> GetCurrencyAsync(int currencyId);
    }
}

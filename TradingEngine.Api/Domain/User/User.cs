using TradingEngine.Api.Domain.Trading;
using TradingEngine.Api.Model.GeneratedContext;

namespace TradingEngine.Api.Domain.User
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public Balance Balance { get; set; }
        
        public virtual void SendMoney(Money money)
        {
            Balance.ChargeMoney(money);
        }

        public virtual void AddMoney(Money money)
        {
            Balance.AddMoney(money);
        }

        public virtual void ExchangeMoney(Money money, Currency toCurrency)
        {
            Balance.Exchange(money, toCurrency);
        }
    }
}

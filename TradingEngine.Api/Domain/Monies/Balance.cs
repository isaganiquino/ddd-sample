using System;
using System.Collections.Generic;
using System.Linq;
using TradingEngine.Api.Model.GeneratedContext;

namespace TradingEngine.Api.Domain.Monies
{
    public class Balance
    {

        private readonly Dictionary<Currency, decimal> _currencies = new Dictionary<Currency, decimal>();

        public virtual void AddMoney(Money money)
        {
            if (_currencies.ContainsKey(money.Currency))
            {
                _currencies[money.Currency] = (_currencies.GetValueOrDefault(money.Currency) + money.Amount);
            }
            else
            {
                _currencies.Add(money.Currency, money.Amount);
            }
        }

        public virtual void ChargeMoney(Money money)
        {
            ValidateMoney(money);
            _currencies[money.Currency] = (_currencies.GetValueOrDefault(money.Currency) - money.Amount);
        }

        public void Exchange(Money money, Currency toCurrency)
        {
            ValidateMoney(money);
            decimal exchangeAmt = (money.Amount / money.Currency.Ratio) * toCurrency.Ratio;
            ChargeMoney(money);
            AddMoney(new Money(){ Currency = toCurrency, Amount = exchangeAmt });
        }

        public List<Money> GetAllMonies()
        {
            return _currencies.Select(c => new Money() {Currency = c.Key, Amount = c.Value }).ToList();
        }

        private void ValidateMoney(Money money)
        {
            decimal currentBalance = _currencies.GetValueOrDefault(money.Currency);

            if (!_currencies.ContainsKey(money.Currency))
            {
                throw new Exception("Currency does not exist!");
            }
            else if (currentBalance < money.Amount)
            {
                throw new Exception("Insufficient amount in balance!");
            }
        }
    }
}

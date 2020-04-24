using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingEngine.Api.Domain.User.Events
{
    public class OnUpdateBalanceEventArgs : EventArgs
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}

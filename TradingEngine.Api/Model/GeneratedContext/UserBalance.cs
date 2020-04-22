using System;
using System.Collections.Generic;

namespace TradingEngine.Api.Model.GeneratedContext
{
    public partial class UserBalance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }

        public virtual User User { get; set; }
    }
}

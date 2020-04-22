using TradingEngine.Api.Model.GeneratedContext;

namespace TradingEngine.Api.Domain.Monies
{
    public class Money
    {
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
    }
}

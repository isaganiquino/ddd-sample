using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingEngine.Api.Implementations.Events;

namespace TradingEngine.Api.Domain.User.Events
{
    public class OnUpdateBalanceEventHandler : ISubscriber<OnUpdateBalanceEventArgs>
    {
        public void Handle(OnUpdateBalanceEventArgs e)
        {
            
        }
    }
}

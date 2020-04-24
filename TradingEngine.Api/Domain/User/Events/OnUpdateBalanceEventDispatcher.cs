using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingEngine.Api.Implementations.Events;

namespace TradingEngine.Api.Domain.User.Events
{
    public class OnUpdateBalanceEventDispatcher
    {
        public static void DispatchOnUpdateBalanceEvent(OnUpdateBalanceEventArgs eventArgs)
        {
            var ea = new EventAggregator();
            ea.Register(new OnUpdateBalanceEventHandler());
            ea.Raise(eventArgs);
        }
    }
}

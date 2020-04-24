using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingEngine.Api.Implementations.Events;

namespace TradingEngine.Api.Domain.User.Events
{
    public class OnUpdateBalanceEventDispatcher
    {
        public static void Dispatch(OnUpdateBalanceEventArgs eventArgs)
        {
            var ea = new EventAggregator();
            ea.Subscribe(new OnUpdateBalanceEventHandler());
            ea.Publish(eventArgs);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingEngine.Api.Implementations.Events
{
    public interface ISubscriber<in T>
    {
        void OnEvent(T e);
    }
}

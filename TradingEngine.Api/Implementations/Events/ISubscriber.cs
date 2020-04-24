using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingEngine.Api.Implementations.Events
{
    interface ISubscriber<T>
    {
        void Handle(T e);
    }
}

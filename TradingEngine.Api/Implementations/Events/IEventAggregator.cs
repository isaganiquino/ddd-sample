using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingEngine.Api.Implementations.Events
{
    public interface IEventAggregator
    {
        void Register(object subscriber);
        void Raise<TEvent>(TEvent eventToPublish);
    }
}

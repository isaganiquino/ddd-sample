using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingEngine.Api.Implementations.Events
{
    public interface IEventAggregator
    {
        void Subscribe(object subscriber);
        void Unsubscribe(object subscriber);
        void Publish<TEvent>(TEvent eventToPublish);
    }
}

using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base.Abstraction
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
        void Subscribe<T, THandler>() where T:IntegrationEvent where THandler : IIntegrationEventHandler<T>;
        void UnSubscribe<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>;
    }
}

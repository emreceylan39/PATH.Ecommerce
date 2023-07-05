using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base.Abstraction
{
    public interface IEventBusSubscriptionManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;
        void AddSubscription<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>;

        void RemoveSubscription<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>;

        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

        bool HasSubscriptionsForEvent(string eventName);
        void Clear();
        IEnumerable<SubscriptionInfo> GetHandlerForEvent<T>() where T : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlerForEvent(string eventName);
        string GetEventKey<T>();
        Type GetEventTypeByName(string eventName);


    }
}

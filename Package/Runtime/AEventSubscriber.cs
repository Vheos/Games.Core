namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    abstract public class AEventSubscriber : ABaseComponent
    {
        // Publics
        public T GetHandler<T>() where T : ABaseComponent
        {
            if (TryGetComponent<T>(out var handler))
                return handler;

            WarningHandlerNotFound(GetType(), typeof(T));
            return gameObject.AddComponent<T>();
        }

        // Privates       
        virtual protected void SubscribeToEvents()
        { }
        protected void SubscribeTo(Event @event, Action action)
        {
            @event.Subscribe(action);
            _subscribedEvents.Add(@event);
        }
        protected void SubscribeTo<T1>(Event<T1> @event, Action<T1> action)
        {
            @event.Subscribe(action);
            _subscribedEvents.Add(@event);
        }
        protected void SubscribeTo<T1, T2>(Event<T1, T2> @event, Action<T1, T2> action)
        {
            @event.Subscribe(action);
            _subscribedEvents.Add(@event);
        }
        protected void SubscribeTo(Event @event, params Action[] actions)
        => SubscribeTo(@event, Delegate.Combine(actions) as Action);
        protected void SubscribeTo<T1>(Event @event, params Action<T1>[] actions)
        => SubscribeTo(@event, Delegate.Combine(actions) as Action<T1>);
        protected void SubscribeTo<T1, T2>(Event @event, params Action<T1, T2>[] actions)
        => SubscribeTo(@event, Delegate.Combine(actions) as Action<T1, T2>);
        private readonly HashSet<AEvent> _subscribedEvents = new HashSet<AEvent>();
        private void WarningHandlerNotFound(Type componentType, Type handlerType)
        => Debug.LogWarning($"HandlerNotFound:\t{name}.{componentType.Name} -> {handlerType.Name}\n" +
        $"Trying to subscribe to a missing handler! Adding the handler as a fallback...");
        private void UnsubscribeFromEvents()
        {
            foreach (var @event in _subscribedEvents)
                @event.Unsubscribe(this);
            _subscribedEvents.Clear();
        }

        // Play
        protected override void PlayEnable()
        {
            base.PlayEnable();
            SubscribeToEvents();
        }
        protected override void PlayDisable()
        {
            base.PlayDisable();
            UnsubscribeFromEvents();
        }
    }
}
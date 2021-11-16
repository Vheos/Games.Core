namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    abstract public class AEventSubscriber : ABaseComponent
    {
        // Privates       
        virtual protected void AutoSubscribeToEvents()
        { }
        protected void SubscribeTo(Event @event, Action action)
        {
            @event.Subscribe(action);
            if (_isAutoSubscribe)
                _subscribedEvents.Add(@event);
        }
        protected void SubscribeTo<T1>(Event<T1> @event, Action<T1> action)
        {
            @event.Subscribe(action);
            if (_isAutoSubscribe)
                _subscribedEvents.Add(@event);
        }
        protected void SubscribeTo<T1, T2>(Event<T1, T2> @event, Action<T1, T2> action)
        {
            @event.Subscribe(action);
            if (_isAutoSubscribe)
                _subscribedEvents.Add(@event);
        }
        protected void SubscribeTo(Event @event, params Action[] actions)
        => SubscribeTo(@event, Delegate.Combine(actions) as Action);
        protected void SubscribeTo<T1>(Event @event, params Action<T1>[] actions)
        => SubscribeTo(@event, Delegate.Combine(actions) as Action<T1>);
        protected void SubscribeTo<T1, T2>(Event @event, params Action<T1, T2>[] actions)
        => SubscribeTo(@event, Delegate.Combine(actions) as Action<T1, T2>);
        protected void UnsubscribeFrom(Event @event)
        => @event.Unsubscribe(this);
        protected void UnsubscribeFrom<T1>(Event<T1> @event)
        => @event.Unsubscribe(this);
        protected void UnsubscribeFrom<T1, T2>(Event<T1, T2> @event)
        => @event.Unsubscribe(this);
        private bool _isAutoSubscribe;
        private readonly HashSet<AEvent> _subscribedEvents = new HashSet<AEvent>();
        private void UnsubscribeFromAllEvents()
        {
            foreach (var @event in _subscribedEvents)
                @event.Unsubscribe(this);
            _subscribedEvents.Clear();
        }

        // Play
        protected override void PlayEnable()
        {
            base.PlayEnable();
            _isAutoSubscribe = true;
            AutoSubscribeToEvents();
            _isAutoSubscribe = false;
        }
        protected override void PlayDisable()
        {
            base.PlayDisable();
            UnsubscribeFromAllEvents();
        }
    }
}
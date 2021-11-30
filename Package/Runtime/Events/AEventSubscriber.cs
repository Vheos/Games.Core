namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    abstract public class AEventSubscriber : ABaseComponent
    {
        // Privates
        virtual protected void DefineAutoSubscriptions()
        { }
        private bool _isWithinAutoSubscribeBlock;
        private readonly HashSet<AEvent> _autoSubscribedEvents = new HashSet<AEvent>();
        private void SubscribeAuto()
        {
            foreach (var @event in _autoSubscribedEvents)
                @event.SubscribeAuto(this);
        }
        private void UnsubscribeAuto()
        {
            foreach (var @event in _autoSubscribedEvents)
                @event.UnsubscribeAuto(this);
        }

        // Privates (subscribe)
        protected void SubscribeTo(Event @event, Action action)
        {
            if (_isWithinAutoSubscribeBlock)
            {
                @event.AddToAutoSubscriptions(this, action);
                _autoSubscribedEvents.Add(@event);
            }
            else
                @event.Subscribe(action);
        }
        protected void SubscribeTo<T1>(Event<T1> @event, Action<T1> action)
        {
            if (_isWithinAutoSubscribeBlock)
            {
                @event.AddToAutoSubscriptions(this, action);
                _autoSubscribedEvents.Add(@event);
            }
            else
                @event.Subscribe(action);
        }
        protected void SubscribeTo<T1, T2>(Event<T1, T2> @event, Action<T1, T2> action)
        {
            if (_isWithinAutoSubscribeBlock)
            {
                @event.AddToAutoSubscriptions(this, action);
                _autoSubscribedEvents.Add(@event);
            }
            else
                @event.Subscribe(action);
        }
        protected void SubscribeTo<T1, T2, T3>(Event<T1, T2, T3> @event, Action<T1, T2, T3> action)
        {
            if (_isWithinAutoSubscribeBlock)
            {
                @event.AddToAutoSubscriptions(this, action);
                _autoSubscribedEvents.Add(@event);
            }
            else
                @event.Subscribe(action);
        }
        protected void UnsubscribeFrom(Event @event, Action action)
        => @event.Unsubscribe(action);
        protected void UnsubscribeFrom<T1>(Event<T1> @event, Action<T1> action)
        => @event.Unsubscribe(action);
        protected void UnsubscribeFrom<T1, T2>(Event<T1, T2> @event, Action<T1, T2> action)
        => @event.Unsubscribe(action);
        protected void UnsubscribeFrom<T1, T2, T3>(Event<T1, T2, T3> @event, Action<T1, T2, T3> action)
        => @event.Unsubscribe(action);

        // Play
        internal protected override void PlayAwakeLate()
        {
            base.PlayAwakeLate();
            _isWithinAutoSubscribeBlock = true;
            DefineAutoSubscriptions();
            _isWithinAutoSubscribeBlock = false;
        }
        protected override void PlayEnable()
        {
            base.PlayEnable();
            SubscribeAuto();
        }
        protected override void PlayDisable()
        {
            base.PlayDisable();
            UnsubscribeAuto();
        }

#if UNITY_EDITOR
        // Debug
        [ContextMenu(nameof(LogAutoSubscriptions))]
        private void LogAutoSubscriptions()
        {
            Debug.Log($"{name}.{GetType().Name} ({_autoSubscribedEvents.Count})");
            foreach (var @event in _autoSubscribedEvents)
                Debug.Log($"\t- {@event.GetType().Name}");
            Debug.Log($"");
        }
#endif
    }
}
namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary> Base class for components that automatically subscribe to events when enabled and unsubscribe when disabled. </summary>
    /// <remarks>
    /// Auto-subscriptions must be defined inside <see cref="DefineAutoSubscriptions"/>.<br/>
    /// Subscriptions defined outside this method have to be managed manually.
    /// </remarks>
    abstract public class AAutoSubscriber : ABaseComponent
    {
        // Privates
        /// <summary> Only subscriptions defined inside this method will be automated. </summary>
        virtual protected void DefineAutoSubscriptions()
        { }
        private bool _isWithinAutoSubscriptionsBlock;
        private readonly HashSet<AAutoEvent> _autoEvents = new HashSet<AAutoEvent>();
        private void SubscribeAuto()
        {
            foreach (var autoEvent in _autoEvents)
                autoEvent.SubscribeAuto(this);
        }
        private void UnsubscribeAuto()
        {
            foreach (var autoEvent in _autoEvents)
                autoEvent.UnsubscribeAuto(this);
        }

        // Privates (subscribe)
        /// <summary> Subscribes to event without any parameters. </summary>
        protected void SubscribeTo(AutoEvent autoEvent, Action action)
        {
            if (_isWithinAutoSubscriptionsBlock)
            {
                autoEvent.AddToAutoSubscriptions(this, action);
                _autoEvents.Add(autoEvent);
            }
            else
                autoEvent.Subscribe(action);
        }
        /// <summary> Subscribes to event with 1 parameter. </summary>
        protected void SubscribeTo<T1>(AutoEvent<T1> autoEvent, Action<T1> action)
        {
            if (_isWithinAutoSubscriptionsBlock)
            {
                autoEvent.AddToAutoSubscriptions(this, action);
                _autoEvents.Add(autoEvent);
            }
            else
                autoEvent.Subscribe(action);
        }
        /// <summary> Subscribes to event with 2 parameters. </summary>
        protected void SubscribeTo<T1, T2>(AutoEvent<T1, T2> autoEvent, Action<T1, T2> action)
        {
            if (_isWithinAutoSubscriptionsBlock)
            {
                autoEvent.AddToAutoSubscriptions(this, action);
                _autoEvents.Add(autoEvent);
            }
            else
                autoEvent.Subscribe(action);
        }
        /// <summary> Subscribes to event with 3 parameters. </summary>
        protected void SubscribeTo<T1, T2, T3>(AutoEvent<T1, T2, T3> autoEvent, Action<T1, T2, T3> action)
        {
            if (_isWithinAutoSubscriptionsBlock)
            {
                autoEvent.AddToAutoSubscriptions(this, action);
                _autoEvents.Add(autoEvent);
            }
            else
                autoEvent.Subscribe(action);
        }
        /// <summary> Unsubscribe from event without any parameters. </summary>
        protected void UnsubscribeFrom(AutoEvent autoEvent, Action action)
        => autoEvent.Unsubscribe(action);
        /// <summary> Unsubscribe from event with 1 parameter. </summary>
        protected void UnsubscribeFrom<T1>(AutoEvent<T1> autoEvent, Action<T1> action)
        => autoEvent.Unsubscribe(action);
        /// <summary> Unsubscribe from event with 1 parameters. </summary>
        protected void UnsubscribeFrom<T1, T2>(AutoEvent<T1, T2> autoEvent, Action<T1, T2> action)
        => autoEvent.Unsubscribe(action);
        /// <summary> Unsubscribe from event with 1 parameters. </summary>
        protected void UnsubscribeFrom<T1, T2, T3>(AutoEvent<T1, T2, T3> autoEvent, Action<T1, T2, T3> action)
        => autoEvent.Unsubscribe(action);

        // Play
        protected private override void PlayAwakeLate()
        {
            base.PlayAwakeLate();
            _isWithinAutoSubscriptionsBlock = true;
            DefineAutoSubscriptions();
            _isWithinAutoSubscriptionsBlock = false;
        }
        /// <inheritdoc cref="APlayable.PlayEnable"/>
        protected override void PlayEnable()
        {
            base.PlayEnable();
            SubscribeAuto();
        }
        /// <inheritdoc cref="APlayable.PlayDisable"/>
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
            Debug.Log($"{name}.{GetType().Name} ({_autoEvents.Count})");
            foreach (var @event in _autoEvents)
                Debug.Log($"\t- {@event.GetType().Name}");
            Debug.Log($"");
        }
#endif
    }
}
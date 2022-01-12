namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary> Base class for components that automatically subscribe to events when enabled and unsubscribe when disabled. </summary>
    /// <remarks>
    /// Auto-subscriptions must be defined inside <c><see cref="DefineAutoSubscriptions"/></c><br/>
    /// Subscriptions defined outside of this method must be managed manually
    /// </remarks>
    abstract public class AAutoSubscriber : ABaseComponent
    {
        // Privates
        /// <summary> Only subscriptions defined inside this method will be automated on component enable/disable. </summary>
        virtual protected void DefineAutoSubscriptions()
        { }
        private readonly HashSet<AAutoEvent> _autoEvents = new HashSet<AAutoEvent>();

        // Privates (subscribe)
        /// <summary> Subscribes <c><paramref name="action"/></c> to <c><paramref name="autoEvent"/></c> </summary>
        /// <remarks> If defined inside <c><see cref="DefineAutoSubscriptions"/></c>, this subscription will be automated on component enable/disable </remarks>
        /// <param name="autoEvent"> When should the <c><paramref name="action"/></c> be called? </param>
        /// <param name="action"> What should happen when the <c><paramref name="autoEvent"/></c> is raised? </param>
        protected void SubscribeAuto(AutoEvent autoEvent, Action action)
        {
            autoEvent.SubscribeAuto(this, action);
            _autoEvents.Add(autoEvent);
        }
        /// <inheritdoc cref="SubscribeAuto"/>
        protected void SubscribeAuto<T1>(AutoEvent<T1> autoEvent, Action<T1> action)
        {
            autoEvent.SubscribeAuto(this, action);
            _autoEvents.Add(autoEvent);
        }
        /// <inheritdoc cref="SubscribeAuto"/>
        protected void SubscribeAuto<T1, T2>(AutoEvent<T1, T2> autoEvent, Action<T1, T2> action)
        {
            autoEvent.SubscribeAuto(this, action);
            _autoEvents.Add(autoEvent);
        }
        /// <inheritdoc cref="SubscribeAuto"/>
        protected void SubscribeAuto<T1, T2, T3>(AutoEvent<T1, T2, T3> autoEvent, Action<T1, T2, T3> action)
        {
            autoEvent.SubscribeAuto(this, action);
            _autoEvents.Add(autoEvent);
        }

        protected void SubscribeUntilInvoke(AutoEvent autoEvent, Action action)
        => autoEvent.SubscribeUntilInvoke(action);
        protected void SubscribeUntilInvoke<T1>(AutoEvent<T1> autoEvent, Action<T1> action)
        => autoEvent.SubscribeUntilInvoke(action);
        protected void SubscribeUntilInvoke<T1, T2>(AutoEvent<T1, T2> autoEvent, Action<T1, T2> action)
        => autoEvent.SubscribeUntilInvoke(action);
        protected void SubscribeUntilInvoke<T1, T2, T3>(AutoEvent<T1, T2, T3> autoEvent, Action<T1, T2, T3> action)
        => autoEvent.SubscribeUntilInvoke(action);

        // Play
        protected override void PlayAwake()
        {
            DefineAutoSubscriptions();
            base.PlayAwake();         
        }
        /// <inheritdoc cref="APlayable.PlayEnable"/>
        protected override void PlayEnable()
        {
            base.PlayEnable();
            foreach (var autoEvent in _autoEvents)
                autoEvent.EnableAutoSubscriptions(this);
        }
        /// <inheritdoc cref="APlayable.PlayDisable"/>
        protected override void PlayDisable()
        {
            base.PlayDisable();
            foreach (var autoEvent in _autoEvents)
                autoEvent.DisableAutoSubscriptions(this);
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
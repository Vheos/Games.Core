namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;

    /// <summary> Base class for events whose subscriptions are automated by <c><see cref="AAutoSubscriber"/></c> components.  </summary>
    abstract public class AAutoEvent
    {
        // Internals
        abstract internal void EnableAutoSubscriptions(AAutoSubscriber subscriber);
        abstract internal void DisableAutoSubscriptions(AAutoSubscriber subscriber);
    }

    /// <summary> Auto-subscription event without any parameters. </summary>
    sealed public class AutoEvent : AAutoEvent
    {
        // Publics
        /// <summary> Invokes all current subscriptions. </summary>
        public void Invoke()
        {
            _internalEvent?.Invoke();
            TryRemoveUntilInvokeActions();
        }

        // Internals
        internal void Subscribe(Action action)
        => _internalEvent += action;
        internal void SubscribeUntilInvoke(Action action)
        {
            _internalEvent += action;
            _untilInvokeActions += action;
        }
        internal void SubscribeAuto(AAutoSubscriber subscriber, Action action, bool isSubscriberAlreadyEnabled)
        {
            if (isSubscriberAlreadyEnabled)
                _internalEvent += action;
            _autoActionsBySubscriber.TryAddDefault(subscriber);
            _autoActionsBySubscriber[subscriber] += action;
        }
        internal override void EnableAutoSubscriptions(AAutoSubscriber subscriber)
        => _internalEvent += _autoActionsBySubscriber[subscriber];
        internal override void DisableAutoSubscriptions(AAutoSubscriber subscriber)
        => _internalEvent -= _autoActionsBySubscriber[subscriber];

        // Privates
        private Action _internalEvent;
        private Action _untilInvokeActions = null;
        private readonly Dictionary<AAutoSubscriber, Action> _autoActionsBySubscriber = new Dictionary<AAutoSubscriber, Action>();
        private void TryRemoveUntilInvokeActions()
        {
            if (_untilInvokeActions == null)
                return;

            _internalEvent -= _untilInvokeActions;
            _untilInvokeActions = null;
        }
    }

    /// <summary> Auto-subscription event with 1 parameter. </summary>
    sealed public class AutoEvent<T1> : AAutoEvent
    {
        // Publics
        /// <summary> Invokes all current subscriptions with given parameter. </summary>
        public void Invoke(T1 arg1)
        {
            _internalEvent?.Invoke(arg1);
            TryRemoveUntilInvokeActions();
        }

        // Internals
        internal void Subscribe(Action<T1> action)
        => _internalEvent += action;
        internal void SubscribeUntilInvoke(Action<T1> action)
        {
            _internalEvent += action;
            _untilInvokeActions += action;
        }
        internal void SubscribeAuto(AAutoSubscriber subscriber, Action<T1> action, bool isSubscriberAlreadyEnabled)
        {
            if (isSubscriberAlreadyEnabled)
                _internalEvent += action;
            _autoActionsBySubscriber.TryAddDefault(subscriber);
            _autoActionsBySubscriber[subscriber] += action;
        }
        internal override void EnableAutoSubscriptions(AAutoSubscriber subscriber)
        => _internalEvent += _autoActionsBySubscriber[subscriber];
        internal override void DisableAutoSubscriptions(AAutoSubscriber subscriber)
        => _internalEvent -= _autoActionsBySubscriber[subscriber];

        // Privates
        private Action<T1> _internalEvent;
        private Action<T1> _untilInvokeActions = null;
        private readonly Dictionary<AAutoSubscriber, Action<T1>> _autoActionsBySubscriber = new Dictionary<AAutoSubscriber, Action<T1>>();
        private void TryRemoveUntilInvokeActions()
        {
            if (_untilInvokeActions == null)
                return;

            _internalEvent -= _untilInvokeActions;
            _untilInvokeActions = null;
        }
    }

    /// <summary> Auto-subscription event with 2 parameters. </summary>
    sealed public class AutoEvent<T1, T2> : AAutoEvent
    {
        // Publics
        /// <summary> Invokes all current subscriptions with given parameters. </summary>
        public void Invoke(T1 arg1, T2 arg2)
        {
            _internalEvent?.Invoke(arg1, arg2);
            TryRemoveUntilInvokeActions();
        }

        // Internals
        internal void Subscribe(Action<T1, T2> action)
        => _internalEvent += action;
        internal void SubscribeUntilInvoke(Action<T1, T2> action)
        {
            _internalEvent += action;
            _untilInvokeActions += action;
        }
        internal void SubscribeAuto(AAutoSubscriber subscriber, Action<T1, T2> action, bool isSubscriberAlreadyEnabled)
        {
            if (isSubscriberAlreadyEnabled)
                _internalEvent += action;
            _autoActionsBySubscriber.TryAddDefault(subscriber);
            _autoActionsBySubscriber[subscriber] += action;
        }
        internal override void EnableAutoSubscriptions(AAutoSubscriber subscriber)
        => _internalEvent += _autoActionsBySubscriber[subscriber];
        internal override void DisableAutoSubscriptions(AAutoSubscriber subscriber)
        => _internalEvent -= _autoActionsBySubscriber[subscriber];

        // Privates
        private Action<T1, T2> _internalEvent;
        private Action<T1, T2> _untilInvokeActions = null;
        private readonly Dictionary<AAutoSubscriber, Action<T1, T2>> _autoActionsBySubscriber = new Dictionary<AAutoSubscriber, Action<T1, T2>>();
        private void TryRemoveUntilInvokeActions()
        {
            if (_untilInvokeActions == null)
                return;

            _internalEvent -= _untilInvokeActions;
            _untilInvokeActions = null;
        }
    }

    /// <summary> Auto-subscription event with 2 parameters. </summary>
    sealed public class AutoEvent<T1, T2, T3> : AAutoEvent
    {
        // Publics
        /// <summary> Invokes all current subscriptions with given parameters. </summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            _internalEvent?.Invoke(arg1, arg2, arg3);
            TryRemoveUntilInvokeActions();
        }

        // Internals
        internal void Subscribe(Action<T1, T2, T3> action)
        => _internalEvent += action;
        internal void SubscribeUntilInvoke(Action<T1, T2, T3> action)
        {
            _internalEvent += action;
            _untilInvokeActions += action;
        }
        internal void SubscribeAuto(AAutoSubscriber subscriber, Action<T1, T2, T3> action, bool isSubscriberAlreadyEnabled)
        {
            if (isSubscriberAlreadyEnabled)
                _internalEvent += action;
            _autoActionsBySubscriber.TryAddDefault(subscriber);
            _autoActionsBySubscriber[subscriber] += action;
        }
        internal override void EnableAutoSubscriptions(AAutoSubscriber subscriber)
        => _internalEvent += _autoActionsBySubscriber[subscriber];
        internal override void DisableAutoSubscriptions(AAutoSubscriber subscriber)
        => _internalEvent -= _autoActionsBySubscriber[subscriber];

        // Privates
        private Action<T1, T2, T3> _internalEvent;
        private Action<T1, T2, T3> _untilInvokeActions = null;
        private readonly Dictionary<AAutoSubscriber, Action<T1, T2, T3>> _autoActionsBySubscriber = new Dictionary<AAutoSubscriber, Action<T1, T2, T3>>();
        private void TryRemoveUntilInvokeActions()
        {
            if (_untilInvokeActions == null)
                return;

            _internalEvent -= _untilInvokeActions;
            _untilInvokeActions = null;
        }
    }
}
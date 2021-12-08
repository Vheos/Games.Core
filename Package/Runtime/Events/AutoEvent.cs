namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;

    /// <summary> Base class for events whose subscriptions are automated by <see cref="AAutoSubscriber"/> components.  </summary>
    abstract public class AAutoEvent
    {
        // Internals
        abstract internal void SubscribeAuto(AAutoSubscriber subscriber);
        abstract internal void UnsubscribeAuto(AAutoSubscriber subscriber);
    }

    /// <summary> Auto-subscription event without any parameters. </summary>
    sealed public class AutoEvent : AAutoEvent
    {
        // Publics
        /// <summary> Invokes all current subscriptions. </summary>
        public void Invoke()
        => _internalEvent?.Invoke();

        // Internals
        internal void Subscribe(Action action)
        => _internalEvent += action;
        internal void Unsubscribe(Action action)
        => _internalEvent -= action;
        internal void AddToAutoSubscriptions(AAutoSubscriber subscriber, Action action)
        {
            _autoActionsBySubscriber.TryAddDefault(subscriber);
            _autoActionsBySubscriber[subscriber] += action;
        }
        internal override void SubscribeAuto(AAutoSubscriber subscriber)
        => _internalEvent += _autoActionsBySubscriber[subscriber];
        internal override void UnsubscribeAuto(AAutoSubscriber subscriber)
        => _internalEvent -= _autoActionsBySubscriber[subscriber];

        // Privates
        private Action _internalEvent;
        private readonly Dictionary<AAutoSubscriber, Action> _autoActionsBySubscriber = new Dictionary<AAutoSubscriber, Action>();
    }

    /// <summary> Auto-subscription event with 1 parameter. </summary>
    sealed public class AutoEvent<T1> : AAutoEvent
    {
        // Publics
        /// <summary> Invokes all current subscriptions with given parameter. </summary>
        public void Invoke(T1 arg1)
        => _internalEvent?.Invoke(arg1);

        // Internals
        internal void Subscribe(Action<T1> action)
       => _internalEvent += action;
        internal void Unsubscribe(Action<T1> action)
        => _internalEvent -= action;
        internal void AddToAutoSubscriptions(AAutoSubscriber subscriber, Action<T1> action)
        {
            _autoActionsBySubscriber.TryAddDefault(subscriber);
            _autoActionsBySubscriber[subscriber] += action;
        }
        internal override void SubscribeAuto(AAutoSubscriber subscriber)
        => _internalEvent += _autoActionsBySubscriber[subscriber];
        internal override void UnsubscribeAuto(AAutoSubscriber subscriber)
        => _internalEvent -= _autoActionsBySubscriber[subscriber];

        // Privates
        private Action<T1> _internalEvent;
        private readonly Dictionary<AAutoSubscriber, Action<T1>> _autoActionsBySubscriber = new Dictionary<AAutoSubscriber, Action<T1>>();
    }

    /// <summary> Auto-subscription event with 2 parameters. </summary>
    sealed public class AutoEvent<T1, T2> : AAutoEvent
    {
        // Publics
        /// <summary> Invokes all current subscriptions with given parameters. </summary>
        public void Invoke(T1 arg1, T2 arg2)
        => _internalEvent?.Invoke(arg1, arg2);

        // Internals
        internal void Subscribe(Action<T1, T2> action)
        => _internalEvent += action;
        internal void Unsubscribe(Action<T1, T2> action)
        => _internalEvent -= action;
        internal void AddToAutoSubscriptions(AAutoSubscriber subscriber, Action<T1, T2> action)
        {
            _autoActionsBySubscriber.TryAddDefault(subscriber);
            _autoActionsBySubscriber[subscriber] += action;
        }
        internal override void SubscribeAuto(AAutoSubscriber subscriber)
        => _internalEvent += _autoActionsBySubscriber[subscriber];
        internal override void UnsubscribeAuto(AAutoSubscriber subscriber)
        => _internalEvent -= _autoActionsBySubscriber[subscriber];

        // Privates
        private Action<T1, T2> _internalEvent;
        private readonly Dictionary<AAutoSubscriber, Action<T1, T2>> _autoActionsBySubscriber            = new Dictionary<AAutoSubscriber, Action<T1, T2>>();
    }

    /// <summary> Auto-subscription event with 3 parameters. </summary>
    sealed public class AutoEvent<T1, T2, T3> : AAutoEvent
    {
        // Publics
        /// <summary> Invokes all current subscriptions with given parameters. </summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        => _internalEvent?.Invoke(arg1, arg2, arg3);

        // Internals
        internal void Subscribe(Action<T1, T2, T3> action)
        => _internalEvent += action;
        internal void Unsubscribe(Action<T1, T2, T3> action)
        => _internalEvent -= action;
        internal void AddToAutoSubscriptions(AAutoSubscriber subscriber, Action<T1, T2, T3> action)
        {
            _autoActionsBySubscriber.TryAddDefault(subscriber);
            _autoActionsBySubscriber[subscriber] += action;
        }
        internal override void SubscribeAuto(AAutoSubscriber subscriber)
        => _internalEvent += _autoActionsBySubscriber[subscriber];
        internal override void UnsubscribeAuto(AAutoSubscriber subscriber)
        => _internalEvent -= _autoActionsBySubscriber[subscriber];

        // Privates
        private Action<T1, T2, T3> _internalEvent;
        private readonly Dictionary<AAutoSubscriber, Action<T1, T2, T3>> _autoActionsBySubscriber            = new Dictionary<AAutoSubscriber, Action<T1, T2, T3>>();
    }
}
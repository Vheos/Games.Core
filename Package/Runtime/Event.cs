namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;

    sealed public class Event : AEvent
    {
        // Publics
        public void Invoke()
        => _internalEvent?.Invoke();

        // Privates
        private Action _internalEvent;
        private readonly Dictionary<AEventSubscriber, Action> _autosBySubscriber
            = new Dictionary<AEventSubscriber, Action>();
        protected override Delegate InternalEvent
        => _internalEvent;
        internal void Subscribe(Action action)
        => _internalEvent += action;
        internal void Unsubscribe(Action action)
        => _internalEvent -= action;
        internal void AddToAutoSubscriptions(AEventSubscriber subscriber, Action action)
        {
            _autosBySubscriber.TryAddDefault(subscriber);
            _autosBySubscriber[subscriber] += action;
        }
        internal override void SubscribeAuto(AEventSubscriber subscriber)
        => _internalEvent += _autosBySubscriber[subscriber];
        internal override void UnsubscribeAuto(AEventSubscriber subscriber)
        => _internalEvent -= _autosBySubscriber[subscriber];
    }

    sealed public class Event<T1> : AEvent
    {
        // Publics
        public void Invoke(T1 arg1)
        => _internalEvent?.Invoke(arg1);

        // Privates
        private Action<T1> _internalEvent;
        private readonly Dictionary<AEventSubscriber, Action<T1>> _autosBySubscriber
            = new Dictionary<AEventSubscriber, Action<T1>>();
        protected override Delegate InternalEvent
        => _internalEvent;
        internal void Subscribe(Action<T1> action)
        => _internalEvent += action;
        internal void Unsubscribe(Action<T1> action)
        => _internalEvent -= action;
        internal void AddToAutoSubscriptions(AEventSubscriber subscriber, Action<T1> action)
        {
            _autosBySubscriber.TryAddDefault(subscriber);
            _autosBySubscriber[subscriber] += action;
        }
        internal override void SubscribeAuto(AEventSubscriber subscriber)
        => _internalEvent += _autosBySubscriber[subscriber];
        internal override void UnsubscribeAuto(AEventSubscriber subscriber)
        => _internalEvent -= _autosBySubscriber[subscriber];
    }

    sealed public class Event<T1, T2> : AEvent
    {
        // Publics
        public void Invoke(T1 arg1, T2 arg2)
        => _internalEvent?.Invoke(arg1, arg2);

        // Privates
        private Action<T1, T2> _internalEvent;
        private readonly Dictionary<AEventSubscriber, Action<T1, T2>> _autosBySubscriber
            = new Dictionary<AEventSubscriber, Action<T1, T2>>();
        protected override Delegate InternalEvent
        => _internalEvent;
        internal void Subscribe(Action<T1, T2> action)
        => _internalEvent += action;
        internal void Unsubscribe(Action<T1, T2> action)
        => _internalEvent -= action;
        internal void AddToAutoSubscriptions(AEventSubscriber subscriber, Action<T1, T2> action)
        {
            _autosBySubscriber.TryAddDefault(subscriber);
            _autosBySubscriber[subscriber] += action;
        }
        internal override void SubscribeAuto(AEventSubscriber subscriber)
        => _internalEvent += _autosBySubscriber[subscriber];
        internal override void UnsubscribeAuto(AEventSubscriber subscriber)
        => _internalEvent -= _autosBySubscriber[subscriber];
    }
}
namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Vheos.Tools.Extensions.General;

    public class AutoEvent
    {
        // Publics
        public void Invoke()
        => _internalEvent?.Invoke();
        public void Subscribe(params Action[] actions)
        {
            foreach (var action in actions)
                _internalEvent += action;
        }
        public void SubscribeOneShot(params Action[] actions)
        {
            _internalEvent += InvokeThenUnsubscribe;
            void InvokeThenUnsubscribe()
            {
                _internalEvent -= InvokeThenUnsubscribe;
                foreach (var action in actions)
                    action();
            }
        }
        public void SubscribeAuto(ABaseComponent subscriber, params Action[] actions)
        {
            var combinedAction = Delegate.Combine(actions).As<Action>();
            subscriber.OnPlayEnable.Subscribe(() => _internalEvent += combinedAction);
            subscriber.OnPlayDisable.Subscribe(() => _internalEvent -= combinedAction);
            if (subscriber.IsEnabled)
                _internalEvent += combinedAction;
        }
        public void Unsubscribe(params Action[] actions)
        {
            foreach (var action in actions)
                _internalEvent -= action;
        }

        // Privates
        private Action _internalEvent;
    }

    public class AutoEvent<T1>
    {
        // Publics
        public void Invoke(T1 arg1)
        => _internalEvent?.Invoke(arg1);
        public void Subscribe(params Action<T1>[] actions)
        {
            foreach (var action in actions)
                _internalEvent += action;
        }
        public void SubscribeOneShot(params Action<T1>[] actions)
        {
            _internalEvent += InvokeThenUnsubscribe;
            void InvokeThenUnsubscribe(T1 arg1)
            {
                _internalEvent -= InvokeThenUnsubscribe;
                foreach (var action in actions)
                    action(arg1);
            }
        }
        public void SubscribeAuto(ABaseComponent subscriber, params Action<T1>[] actions)
        {
            var combinedAction = Delegate.Combine(actions).As<Action<T1>>();
            subscriber.OnPlayEnable.Subscribe(() => _internalEvent += combinedAction);
            subscriber.OnPlayDisable.Subscribe(() => _internalEvent -= combinedAction);
            if (subscriber.IsEnabled)
                _internalEvent += combinedAction;
        }
        public void Unsubscribe(params Action<T1>[] actions)
        {
            foreach (var action in actions)
                _internalEvent -= action;
        }

        // Privates
        private Action<T1> _internalEvent;
    }

    public class AutoEvent<T1, T2>
    {
        // Publics
        public void Invoke(T1 arg1, T2 arg2)
        => _internalEvent?.Invoke(arg1, arg2);
        public void Subscribe(params Action<T1, T2>[] actions)
        {
            foreach (var action in actions)
                _internalEvent += action;
        }
        public void SubscribeOneShot(params Action<T1, T2>[] actions)
        {
            _internalEvent += InvokeThenUnsubscribe;
            void InvokeThenUnsubscribe(T1 arg1, T2 arg2)
            {
                _internalEvent -= InvokeThenUnsubscribe;
                foreach (var action in actions)
                    action(arg1, arg2);
            }
        }
        public void SubscribeAuto(ABaseComponent subscriber, params Action<T1, T2>[] actions)
        {
            var combinedAction = Delegate.Combine(actions).As<Action<T1, T2>>();
            subscriber.OnPlayEnable.Subscribe(() => _internalEvent += combinedAction);
            subscriber.OnPlayDisable.Subscribe(() => _internalEvent -= combinedAction);
            if (subscriber.IsEnabled)
                _internalEvent += combinedAction;
        }
        public void Unsubscribe(params Action<T1, T2>[] actions)
        {
            foreach (var action in actions)
                _internalEvent -= action;
        }

        // Privates
        private Action<T1, T2> _internalEvent;
    }

    public class AutoEvent<T1, T2, T3>
    {
        // Publics
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        => _internalEvent?.Invoke(arg1, arg2, arg3);
        public void Subscribe(params Action<T1, T2, T3>[] actions)
        {
            foreach (var action in actions)
                _internalEvent += action;
        }
        public void SubscribeOneShot(params Action<T1, T2, T3>[] actions)
        {
            _internalEvent += InvokeThenUnsubscribe;
            void InvokeThenUnsubscribe(T1 arg1, T2 arg2, T3 arg3)
            {
                _internalEvent -= InvokeThenUnsubscribe;
                foreach (var action in actions)
                    action(arg1, arg2, arg3);
            }
        }
        public void SubscribeAuto(ABaseComponent subscriber, params Action<T1, T2, T3>[] actions)
        {
            var combinedAction = Delegate.Combine(actions).As<Action<T1, T2, T3>>();
            subscriber.OnPlayEnable.Subscribe(() => _internalEvent += combinedAction);
            subscriber.OnPlayDisable.Subscribe(() => _internalEvent -= combinedAction);
            if (subscriber.IsEnabled)
                _internalEvent += combinedAction;
        }
        public void Unsubscribe(params Action<T1, T2, T3>[] actions)
        {
            foreach (var action in actions)
                _internalEvent -= action;
        }

        // Privates
        private Action<T1, T2, T3> _internalEvent;
    }
}
namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Vheos.Tools.Extensions.General;

    public class AutoEvent
    {
        // Publics - General
        public void Invoke()
        => _internalEvent?.Invoke();
        public void Sub(params Action[] actions)
        {
            foreach (var action in actions)
                _internalEvent += action;
        }
        public void Unsub(params Action[] actions)
        {
            foreach (var action in actions)
                _internalEvent -= action;
        }

        // Publics - Auto
        public void SubOnce(params Action[] actions)
        {
            _internalEvent += InvokeThenUnsubscribe;
            void InvokeThenUnsubscribe()
            {
                _internalEvent -= InvokeThenUnsubscribe;
                foreach (var action in actions)
                    action?.Invoke();
            }
        }
        public void SubDestroy(ABaseComponent subscriber, params Action[] actions)
        {
            var combinedAction = Delegate.Combine(actions) as Action;
            _internalEvent += combinedAction;
            subscriber.OnPlayDestroy.Sub(() => _internalEvent -= combinedAction);
        }
        public void SubEnableDisable(ABaseComponent subscriber, params Action[] actions)
        {
            var combinedAction = Delegate.Combine(actions) as Action;
            subscriber.OnPlayEnable.Sub(() => _internalEvent += combinedAction);
            subscriber.OnPlayDisable.Sub(() => _internalEvent -= combinedAction);
            if (subscriber.IsBetweenEnableAndDisable)
                _internalEvent += combinedAction;
        }

        // Privates
        private Action _internalEvent;
    }

    public class AutoEvent<T1>
    {
        // Publics - General
        public void Invoke(T1 arg1)
        => _internalEvent?.Invoke(arg1);
        public void Sub(params Action<T1>[] actions)
        {
            foreach (var action in actions)
                _internalEvent += action;
        }
        public void Unsub(params Action<T1>[] actions)
        {
            foreach (var action in actions)
                _internalEvent -= action;
        }

        // Publics - Auto
        public void SubOnce(params Action<T1>[] actions)
        {
            _internalEvent += InvokeThenUnsubscribe;
            void InvokeThenUnsubscribe(T1 arg1)
            {
                _internalEvent -= InvokeThenUnsubscribe;
                foreach (var action in actions)
                    action?.Invoke(arg1);
            }
        }
        public void SubDestroy(ABaseComponent subscriber, params Action[] actions)
        {
            var combinedAction = Delegate.Combine(actions) as Action<T1>;
            _internalEvent += combinedAction;
            subscriber.OnPlayDestroy.Sub(() => _internalEvent -= combinedAction);
        }
        public void SubEnableDisable(ABaseComponent subscriber, params Action<T1>[] actions)
        {
            var combinedAction = Delegate.Combine(actions) as Action<T1>;
            subscriber.OnPlayEnable.Sub(() => _internalEvent += combinedAction);
            subscriber.OnPlayDisable.Sub(() => _internalEvent -= combinedAction);
            if (subscriber.IsBetweenEnableAndDisable)
                _internalEvent += combinedAction;
        }

        // Privates
        private Action<T1> _internalEvent;
    }

    public class AutoEvent<T1, T2>
    {
        // Publics - General
        public void Invoke(T1 arg1, T2 arg2)
        => _internalEvent?.Invoke(arg1, arg2);
        public void Sub(params Action<T1, T2>[] actions)
        {
            foreach (var action in actions)
                _internalEvent += action;
        }
        public void Unsub(params Action<T1, T2>[] actions)
        {
            foreach (var action in actions)
                _internalEvent -= action;
        }

        // Publics - Auto
        public void SubOnce(params Action<T1, T2>[] actions)
        {
            _internalEvent += InvokeThenUnsubscribe;
            void InvokeThenUnsubscribe(T1 arg1, T2 arg2)
            {
                _internalEvent -= InvokeThenUnsubscribe;
                foreach (var action in actions)
                    action?.Invoke(arg1, arg2);
            }
        }
        public void SubDestroy(ABaseComponent subscriber, params Action[] actions)
        {
            var combinedAction = Delegate.Combine(actions) as Action<T1, T2>;
            _internalEvent += combinedAction;
            subscriber.OnPlayDestroy.Sub(() => _internalEvent -= combinedAction);
        }
        public void SubEnableDisable(ABaseComponent subscriber, params Action<T1, T2>[] actions)
        {
            var combinedAction = Delegate.Combine(actions) as Action<T1, T2>;
            subscriber.OnPlayEnable.Sub(() => _internalEvent += combinedAction);
            subscriber.OnPlayDisable.Sub(() => _internalEvent -= combinedAction);
            if (subscriber.IsBetweenEnableAndDisable)
                _internalEvent += combinedAction;
        }

        // Privates
        private Action<T1, T2> _internalEvent;
    }

    public class AutoEvent<T1, T2, T3>
    {
        // Publics - General
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        => _internalEvent?.Invoke(arg1, arg2, arg3);
        public void Sub(params Action<T1, T2, T3>[] actions)
        {
            foreach (var action in actions)
                _internalEvent += action;
        }
        public void Unsub(params Action<T1, T2, T3>[] actions)
        {
            foreach (var action in actions)
                _internalEvent -= action;
        }

        // Publics - Auto
        public void SubOnce(params Action<T1, T2, T3>[] actions)
        {
            _internalEvent += InvokeThenUnsubscribe;
            void InvokeThenUnsubscribe(T1 arg1, T2 arg2, T3 arg3)
            {
                _internalEvent -= InvokeThenUnsubscribe;
                foreach (var action in actions)
                    action?.Invoke(arg1, arg2, arg3);
            }
        }
        public void SubDestroy(ABaseComponent subscriber, params Action[] actions)
        {
            var combinedAction = Delegate.Combine(actions) as Action<T1, T2, T3>;
            _internalEvent += combinedAction;
            subscriber.OnPlayDestroy.Sub(() => _internalEvent -= combinedAction);
        }
        public void SubEnableDisable(ABaseComponent subscriber, params Action<T1, T2, T3>[] actions)
        {
            var combinedAction = Delegate.Combine(actions) as Action<T1, T2, T3>;
            subscriber.OnPlayEnable.Sub(() => _internalEvent += combinedAction);
            subscriber.OnPlayDisable.Sub(() => _internalEvent -= combinedAction);
            if (subscriber.IsBetweenEnableAndDisable)
                _internalEvent += combinedAction;
        }

        // Privates
        private Action<T1, T2, T3> _internalEvent;
    }
}
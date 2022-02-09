namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

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
            Sub(actions);
            subscriber.OnPlayDestroy.Sub(() => Unsub(actions));
        }
        public void SubEnableDisable(ABaseComponent subscriber, params Action[] actions)
        {
            subscriber.OnPlayEnable.Sub(() => Sub(actions));
            subscriber.OnPlayDisable.Sub(() => Unsub(actions));
            if (subscriber.IsBetweenEnableAndDisable)
                Sub(actions);
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
        public void SubDestroy(ABaseComponent subscriber, params Action<T1>[] actions)
        {
            Sub(actions);
            subscriber.OnPlayDestroy.Sub(() => Unsub(actions));
        }
        public void SubEnableDisable(ABaseComponent subscriber, params Action<T1>[] actions)
        {
            subscriber.OnPlayEnable.Sub(() => Sub(actions));
            subscriber.OnPlayDisable.Sub(() => Unsub(actions));
            if (subscriber.IsBetweenEnableAndDisable)
                Sub(actions);
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
        public void SubDestroy(ABaseComponent subscriber, params Action<T1, T2>[] actions)
        {
            Sub(actions);
            subscriber.OnPlayDestroy.Sub(() => Unsub(actions));
        }
        public void SubEnableDisable(ABaseComponent subscriber, params Action<T1, T2>[] actions)
        {
            subscriber.OnPlayEnable.Sub(() => Sub(actions));
            subscriber.OnPlayDisable.Sub(() => Unsub(actions));
            if (subscriber.IsBetweenEnableAndDisable)
                Sub(actions);
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
        public void SubDestroy(ABaseComponent subscriber, params Action<T1, T2, T3>[] actions)
        {
            Sub(actions);
            subscriber.OnPlayDestroy.Sub(() => Unsub(actions));
        }
        public void SubEnableDisable(ABaseComponent subscriber, params Action<T1, T2, T3>[] actions)
        {
            subscriber.OnPlayEnable.Sub(() => Sub(actions));
            subscriber.OnPlayDisable.Sub(() => Unsub(actions));
            if (subscriber.IsBetweenEnableAndDisable)
                Sub(actions);
        }

        // Privates
        private Action<T1, T2, T3> _internalEvent;
    }
}
namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.General;
    using Tools.Extensions.Math;
    using Tools.Extensions.UnityObjects;

    public abstract class AUserOfOne<TUser, TUsable> : AUser<TUser, TUsable>
        where TUser : AUserOfOne<TUser, TUsable>
        where TUsable : AUsable<TUsable, TUser>
    {
        // Overrides
        override public void ClearUsables(Func<TUsable, bool> test = null)
        {
            if (!IsUsingAny)
                return;

            if (test == null || test(_usable))
                SetUsable(null);
        }
        override protected bool IsUsingAny
        => _usable != null;
        override protected bool IsUsing(TUsable usable)
        => _usable == usable;
        override protected bool IsUsing<T>()
        => _usable != null && _usable.Has<T>();

        // Privates
        protected readonly AutoEvent<TUser> OnChangeUsable = new();
        protected TUsable _usable;
        protected void SetUsable(TUsable newUsable)
        {
            TUser asTUser = this as TUser;
            TUsable previousUsable = _usable;
            _usable = newUsable;
            OnChangeUsable.Invoke(asTUser);

            if (previousUsable != null)
                previousUsable.StopBeingUsedBy(asTUser);
            if (_usable != null)
                _usable.StartBeingUsedBy(asTUser);
        }
        protected bool TrySetUsable(TUsable newUsable)
        {
            if (newUsable == _usable
            || newUsable != null && !PerformAllTests(this as TUser, newUsable))
                return false;

            SetUsable(newUsable);
            return true;
        }
        protected bool TryGetUsable(out TUsable usable)
        => _usable.TryNonNull(out usable);
        protected bool TryGetUsable<T>(out T component) where T : Component
        {
            if (_usable != null
            && _usable.TryGet(out component))
                return true;

            component = default;
            return false;
        }
    }
}
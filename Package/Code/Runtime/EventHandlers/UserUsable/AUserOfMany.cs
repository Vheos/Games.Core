namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.General;
    using Tools.Extensions.Math;
    using Tools.Extensions.UnityObjects;

    public abstract class AUserOfMany<TUser, TUsable> : AUser<TUser, TUsable>
        where TUser : AUserOfMany<TUser, TUsable>
        where TUsable : AUsable<TUsable, TUser>
    {
        // Overrides
        override public void ClearUsables(Func<TUsable, bool> test = null)
        {
            if (!IsUsingAny)
                return;

            foreach (var usable in _usables.MakeCopy())
                if (test == null || test(usable))
                    RemoveUsable(usable);
        }
        override protected bool IsUsingAny
        => _usables.Count > 0;
        override protected bool IsUsing(TUsable usable)
        => _usables.Contains(usable);
        override protected bool IsUsing<T>()
        => _usables.Any(t => t.Has<T>());

        // Privates
        protected readonly AutoEvent<TUser, TUsable> OnAddUsable = new();
        protected readonly AutoEvent<TUser, TUsable> OnRemoveUsable = new();
        protected HashSet<TUsable> _usables = new();
        protected void AddUsable(TUsable usable)
        {
            var asTUser = this as TUser;
            _usables.Add(usable);
            OnAddUsable.Invoke(asTUser, usable);
            usable.StartBeingUsedBy(asTUser);
        }
        protected void RemoveUsable(TUsable usable)
        {
            var asTUser = this as TUser;
            _usables.Remove(usable);
            OnRemoveUsable.Invoke(asTUser, usable);
            usable.StopBeingUsedBy(asTUser);
        }
        protected bool TryAddUsable(TUsable usable)
        {
            if (usable == null
            || _usables.Contains(usable)
            || !PerformAllTests(this as TUser, usable))
                return false;

            AddUsable(usable);
            return true;
        }
        protected bool TryRemoveUsable(TUsable usable)
        {
            if (usable == null
            || !_usables.Contains(usable))
                return false;

            RemoveUsable(usable);
            return true;
        }
    }
}
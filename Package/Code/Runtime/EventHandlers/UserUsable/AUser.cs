namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.General;
    using Tools.Extensions.Math;
    using Tools.Extensions.UnityObjects;

    public abstract class AUser<TUser, TUsable> : ABaseComponent
        where TUser : AUser<TUser, TUsable>
        where TUsable : AUsable<TUsable, TUser>
    {
        // Abstract
        abstract public void ClearUsables(Func<TUsable, bool> test = null);
        abstract protected bool IsUsingAny
        { get; }
        abstract protected bool IsUsing(TUsable usable);
        abstract protected bool IsUsing<T>() where T : Component;

        // Common
        public void AddTest(Func<TUsable, bool> test)
        {
            _usableTests.Add(test);
            ClearUsables(t => !CanUse(t));
        }
        public void RemoveTest(Func<TUsable, bool> test)
        => _usableTests.Remove(test);
        protected bool CanUse(TUsable usable)
        {
            foreach (var test in _usableTests)
                if (!test(usable))
                    return false;
            return true;
        }
        static internal bool PerformAllTests(TUser user, TUsable usable)
        => user.isActiveAndEnabled && usable.isActiveAndEnabled
        && user.CanUse(usable) && usable.CanBeUsedBy(user);
        private readonly HashSet<Func<TUsable, bool>> _usableTests = new();

        // Play
        protected override void PlayDisable()
        {
            base.PlayDisable();
            ClearUsables();
        }
    }
}
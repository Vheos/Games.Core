namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    public abstract class AUsable<TUsable, TUser> : ABaseComponent
        where TUsable : AUsable<TUsable, TUser>
        where TUser : AUser<TUser, TUsable>
    {
        // Abstract
        abstract public void ClearUsers(Func<TUser, bool> test = null);
        abstract internal bool IsBeingUsed
        { get; }
        abstract internal bool IsBeingUsedBy(TUser user);
        abstract internal void StartBeingUsedBy(TUser user);
        abstract internal void StopBeingUsedBy(TUser user);

        // Common
        public void AddTest(Func<TUser, bool> test)
        {
            _userTests.Add(test);
            ClearUsers(t => !CanBeUsedBy(t));
        }
        public void RemoveTest(Func<TUser, bool> test)
        => _userTests.Remove(test);
        internal bool CanBeUsedBy(TUser user)
        {
            foreach (var test in _userTests)
                if (!test(user))
                    return false;
            return true;
        }
        protected readonly HashSet<Func<TUser, bool>> _userTests = new();
        protected readonly AutoEvent<TUser> OnStartBeingUsed = new();
        protected readonly AutoEvent<TUser> OnStopBeingUsed = new();

        // Play
        protected override void PlayDisable()
        {
            base.PlayDisable();
            ClearUsers();
        }
    }
}
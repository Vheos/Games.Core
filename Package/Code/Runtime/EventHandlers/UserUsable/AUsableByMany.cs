namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    public abstract class AUsableByMany<TUsable, TUser> : AUsable<TUsable, TUser>
        where TUsable : AUsableByMany<TUsable, TUser>
        where TUser : AUser<TUser, TUsable>
    {
        // Overrides
        override public void ClearUsers(Func<TUser, bool> test = null)
        {
            if (!IsBeingUsed)
                return;

            foreach (var user in _users.MakeCopy())
                if (test == null || test(user))
                    user.ClearUsables();
        }
        override protected internal bool IsBeingUsed
        => _users.Count > 0;
        override protected internal bool IsBeingUsedBy(TUser user)
        => _users.Contains(user);
        override protected internal void StartBeingUsedBy(TUser user)
        {
            _users.Add(user);
            OnStartBeingUsed.Invoke(this as TUsable, user);
        }
        override protected internal void StopBeingUsedBy(TUser user)
        {
            _users.Remove(user);
            OnStopBeingUsed.Invoke(this as TUsable, user);
        }

        // Privates
        protected bool IsBeingUsedByMany
        => _users.Count == 0;
        protected readonly HashSet<TUser> _users = new();
    }
}
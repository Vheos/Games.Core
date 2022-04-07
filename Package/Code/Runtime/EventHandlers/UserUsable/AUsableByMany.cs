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
        override internal bool IsBeingUsed
        => _users.Count > 0;
        override internal bool IsBeingUsedBy(TUser user)
        => _users.Contains(user);
        override internal void StartBeingUsedBy(TUser user)
        {
            _users.Add(user);
            OnStartBeingUsed.Invoke(user);
        }
        override internal void StopBeingUsedBy(TUser user)
        {
            _users.Remove(user);
            OnStopBeingUsed.Invoke(user);
        }

        // Privates
        protected readonly HashSet<TUser> _users = new();
    }
}
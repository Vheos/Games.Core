namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    public abstract class AUsableByOne<TUsable, TUser> : AUsable<TUsable, TUser>
        where TUsable : AUsableByOne<TUsable, TUser>
        where TUser : AUser<TUser, TUsable>
    {
        // Overrides
        override public void ClearUsers(Func<TUser, bool> test = null)
        {
            if (!IsBeingUsed)
                return;

            if (test == null || test(_user))
                _user.ClearUsables();
        }
        override protected internal bool IsBeingUsed
        => _user != null;
        override protected internal bool IsBeingUsedBy(TUser user)
        => _user == user;
        override protected internal void StartBeingUsedBy(TUser user)
        {
            _user = user;
            OnStartBeingUsed.Invoke(this as TUsable, user);
        }
        override protected internal void StopBeingUsedBy(TUser user)
        {
            var previouUser = _user;
            _user = null;
            OnStopBeingUsed.Invoke(this as TUsable, previouUser);
        }

        // Privates
        protected TUser _user;
        protected void StopBeingUsed()
        => StopBeingUsedBy(null);
    }
}
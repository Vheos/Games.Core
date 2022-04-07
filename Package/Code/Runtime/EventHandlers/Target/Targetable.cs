namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    public class Targetable : AUsableByMany<Targetable, Targeter>
    {
        // Events
        /// <summary>
        /// <c><see cref="Targeter"/></c> - the component that started targeting this targetable<br/>
        /// <c><see cref="bool"/></c> - whether the above component is the only one targeting this targetable
        /// </summary>
        public readonly AutoEvent<Targeter, bool> OnGainTargeting = new();
        /// <summary>
        /// <c><see cref="Targeter"/></c> - the component that stopped targeting this targetable<br/>
        /// <c><see cref="bool"/></c> - whether the above component was the only one targeting this targetable
        /// </summary>
        public readonly AutoEvent<Targeter, bool> OnLoseTargeting = new();

        // Publics
        public IReadOnlyCollection<Targeter> Targeters
        => _users;
        public bool IsTargeted
        => IsBeingUsed;
        public bool IsTargetedBy(Targeter selecter)
        => IsBeingUsedBy(selecter);

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            OnStartBeingUsed.SubEnableDisable(this, user => OnGainTargeting.Invoke(user, _users.Count == 1));
            OnStopBeingUsed.SubEnableDisable(this, user => OnLoseTargeting.Invoke(user, _users.Count == 0));
        }
    }
}
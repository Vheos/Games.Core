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
        /// <c><see cref="Targeter"/></c> - the component that raised this event<br/>
        /// <c><see cref="Targeter"/></c> - the component that started targeting this targetable
        /// </summary>
        public AutoEvent<Targetable, Targeter> OnGainTargeting
        => OnStartBeingUsed;
        /// <summary>
        /// <c><see cref="Targeter"/></c> - the component that raised this event<br/>
        /// <c><see cref="Targeter"/></c> - the component that stopped targeting this targetable
        /// </summary>
        public AutoEvent<Targetable, Targeter> OnLoseTargeting
        => OnStopBeingUsed;

        // Publics
        public IReadOnlyCollection<Targeter> Targeters
        => _users;
        public bool IsTargeted
        => IsBeingUsed;
        public bool IsTargetedBy(Targeter selecter)
        => IsBeingUsedBy(selecter);
        public bool IsTargetedByMany
        => IsBeingUsedByMany;
    }
}
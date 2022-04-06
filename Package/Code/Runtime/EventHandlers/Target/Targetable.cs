namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    public class Targetable : AMultiReceptor<Targetable, Targeter>
    {
        // Events
        public AutoEvent<Targeter, bool> OnGainTargeting
        => OnGainEffect;
        public AutoEvent<Targeter, bool> OnLoseTargeting
        => OnLoseEffect;

        // Publics
        public IReadOnlyCollection<Targeter> Targeters
        => _effectors;
        public bool IsTargeted
        => IsEffected;
        public bool IsTargetedBy(Targeter selecter)
        => IsEffectedBy(selecter);
    }
}
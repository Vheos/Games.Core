namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Targetable : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Targeter, bool> OnGainTargeting = new();
        public readonly AutoEvent<Targeter, bool> OnLoseTargeting = new();

        // Publics
        public IReadOnlyList<Targeter> Targeters
        => _targeters;
        public void ClearAllTargeting()
        {
            foreach(var targeter in _targeters.MakeCopy())
                TryLoseTargetingFrom(targeter);
        }

        // Privates
        private readonly List<Targeter> _targeters = new();
        internal void TryGainTargetingFrom(Targeter targeter)
        {
            if (_targeters.TryAddUnique(targeter))
                OnGainTargeting?.Invoke(targeter, _targeters.Count == 1);   // is first
        }
        internal void TryLoseTargetingFrom(Targeter targeter)
        {
            if (_targeters.TryRemove(targeter))
                OnLoseTargeting?.Invoke(targeter, _targeters.Count == 0);   // was last
        }
    }
}
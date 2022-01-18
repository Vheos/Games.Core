namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Targetable : AAutoSubscriber
    {
        // Events
        public AutoEvent<Targeter, bool> OnGainTargeting
        { get; } = new AutoEvent<Targeter, bool>();
        public AutoEvent<Targeter, bool> OnLoseTargeting
        { get; } = new AutoEvent<Targeter, bool>();

        // Publics
        public IReadOnlyList<Targeter> Targeters
        => _targeters;
        public void ClearAllTargeting()
        {
            foreach(var targeter in _targeters.MakeCopy())
                TryLoseTargetingFrom(targeter);
        }

        // Privates
        private readonly List<Targeter> _targeters = new List<Targeter>();
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
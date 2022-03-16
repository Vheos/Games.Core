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
        public IReadOnlyCollection<Targeter> Targeters
        => _targeters;
        public bool IsTargeted
        => _targeters.Count != 0;
        public bool IsTargetedBy(Targeter targeter)
        => _targeters.Contains(targeter);
        public void ClearTargeting()
        {
            if (IsTargeted)
                foreach (var targeter in _targeters.MakeCopy())
                {
                    _targeters.Remove(targeter);
                    OnLoseTargeting.Invoke(targeter, _targeters.Count == 0);
                }
        }

        // Internals
        internal void GetTargetedBy(Targeter targeter)
        {
            _targeters.Add(targeter);
            OnGainTargeting.Invoke(targeter, _targeters.Count == 1);   // is first
        }
        internal void GetUntargetedBy(Targeter targeter)
        {
            _targeters.Remove(targeter);
            OnLoseTargeting.Invoke(targeter, _targeters.Count == 0);   // is last
        }

        // Privates
        private readonly HashSet<Targeter> _targeters = new();

        // Play
        protected override void PlayDisable()
        {
            base.PlayDisable();
            ClearTargeting();
        }
    }
}
namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Targeter : ABaseComponent
    {
        // Publics

        public Targetable Targetable
        {
            get => _targetable;
            set
            {
                Targetable previousTarget = _targetable;
                _targetable = value;

                if (previousTarget != _targetable)
                {
                    if (previousTarget != null)
                        previousTarget.TryLoseTargetingFrom(this);
                    if (_targetable != null)
                        _targetable.TryGainTargetingFrom(this);
                }
            }
        }
        public bool IsTargetingAny
        => _targetable != null;
        public bool IsTargeting(Targetable targetable)
        => _targetable == targetable;

        // Privates
        private Targetable _targetable;
    }
}
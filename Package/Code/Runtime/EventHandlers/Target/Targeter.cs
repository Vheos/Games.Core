namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Targeter : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Targetable, Targetable> OnChangeTargetable = new();

        // Publics
        public Targetable Targetable
        {
            get => _targetable;
            set
            {
                Targetable previousTargetable = _targetable;
                _targetable = value;

                if (previousTargetable != _targetable)
                {
                    if (previousTargetable != null)
                        previousTargetable.TryLoseTargetingFrom(this);
                    if (_targetable != null)
                        _targetable.TryGainTargetingFrom(this);
                    OnChangeTargetable.Invoke(previousTargetable, _targetable);
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
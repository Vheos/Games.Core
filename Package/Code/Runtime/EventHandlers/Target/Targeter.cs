namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Targeter : ABaseComponent
    {
        // Publics
        private Targetable _targetable;
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
    }
}
namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Targeter : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Targetable, Targetable> OnChangeTarget = new();

        // Publics
        private Targetable _target;
        public Targetable Target
        {
            get => _target;
            set
            {
                Targetable previousTarget = _target;
                _target = value;

                if (previousTarget != _target)
                {
                    OnChangeTarget?.Invoke(previousTarget, _target);
                    if (previousTarget != null)
                        previousTarget.TryLoseTargetingFrom(this);
                    if (_target != null)
                        _target.TryGainTargetingFrom(this);
                }
            }
        }
    }
}
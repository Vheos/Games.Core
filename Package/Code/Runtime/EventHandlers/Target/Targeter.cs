namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
        using Tools.Extensions.Math;
    using Tools.Extensions.UnityObjects;

    [DisallowMultipleComponent]
    sealed public class Targeter : AAutoSubscriber
    {
        // Events
        public AutoEvent<Targetable, Targetable> OnChangeTarget
        { get; } = new AutoEvent<Targetable, Targetable>();

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
                        previousTarget.LoseTargetingFrom(this);
                    if (_target != null)
                        _target.GainTargetingFrom(this);
                }
            }
        }
    }
}
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
                if (value != null && !value.CanGetTargetedBy(this)
                || value == null && _targetable == null)
                    return;

                Targetable previousTargetable = _targetable;
                if (previousTargetable != null
                && previousTargetable.CanGetUntargetedBy(this))
                    previousTargetable.GetUntargetedBy(this);

                _targetable = value;
                if (_targetable != null)
                    _targetable.GetTargetedBy(this);

                OnChangeTargetable.Invoke(previousTargetable, _targetable);        
            }
        }
        public bool TryGetTargetable<T>(out T component) where T : Component
        {
            if (_targetable != null
            && _targetable.TryGet(out component))
                return true;

            component = default;
            return false;
        }
        public bool IsTargetingAny
        => _targetable != null;
        public bool IsTargeting(Targetable targetable)
        => _targetable == targetable;
        public bool IsTargeting<T>() where T : Component
        => _targetable != null && _targetable.Has<T>();

        // Privates
        private Targetable _targetable;
    }
}
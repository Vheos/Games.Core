namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
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
                if (value == _targetable
                || value != null && (!value.isActiveAndEnabled || value.IsTargetedBy(this) || !CanTarget(value)))
                    return;

                Targetable previousTargetable = _targetable;
                if (previousTargetable != null
                && previousTargetable.IsTargetedBy(this))
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
        public void AddTargetingTest(Func<Targetable, bool> test)
        => _targetingTests.Add(test);
        public void RemoveTargetingTest(Func<Targetable, bool> test)
        => _targetingTests.Remove(test);

        // Privates
        private Targetable _targetable;
        private readonly HashSet<Func<Targetable, bool>> _targetingTests = new();
        private bool CanTarget(Targetable targetable)
        {
            foreach (var test in _targetingTests)
                if (!test(targetable))
                    return false;
            return true;
        }
    }
}
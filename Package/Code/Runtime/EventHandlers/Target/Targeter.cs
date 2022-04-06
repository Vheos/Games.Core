namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Targeter : ASingleEffector<Targeter, Targetable>
    {
        // Events
        public AutoEvent<Targetable, Targetable> OnChangeTargetable
        => OnChangeReceptor;

        // Publics
        public Targetable Targetable
        {
            get => _receptor;
            set => SetReceptor(value);
        }
        public bool IsTargetingAny
        => IsEffectingAny;
        public bool IsTargeting(Targetable targetable)
        => IsEffecting(targetable);
        public bool IsTargeting<T>() where T : Component
        => IsEffecting<T>();
        public bool TryGetTargetable(out Targetable component)
        => TryGetReceptor(out component);
        public bool TryGetTargetable<T>(out T component) where T : Component
        => TryGetReceptor(out component);
    }
}
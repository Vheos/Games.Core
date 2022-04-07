namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Targeter : AUserOfOne<Targeter, Targetable>
    {
        // Events
        public AutoEvent<Targetable, Targetable> OnChangeTargetable
        => OnChangeUser;

        // Publics
        public Targetable Targetable
        {
            get => _usable;
            set => TrySetUsable(value);
        }
        public bool IsTargetingAny
        => IsUsingAny;
        public bool IsTargeting(Targetable targetable)
        => IsUsing(targetable);
        public bool IsTargeting<T>() where T : Component
        => IsUsing<T>();
        public bool TryGetTargetable(out Targetable component)
        => TryGetUsable(out component);
        public bool TryGetTargetable<T>(out T component) where T : Component
        => TryGetUsable(out component);
    }
}
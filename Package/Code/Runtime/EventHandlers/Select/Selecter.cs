namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Selecter : ASingleEffector<Selecter, Selectable>
    {
        // Events
        public AutoEvent<Selectable, Selectable> OnChangeSelectable
        => OnChangeReceptor;

        // Publics
        public Selectable Selectable
        {
            get => _receptor;
            set => SetReceptor(value);
        }
        public bool IsSelectingAny
        => IsEffectingAny;
        public bool IsSelecting(Selectable selectable)
        => IsEffecting(selectable);
        public bool IsSelecting<T>() where T : Component
        => IsSelecting<T>();
        public bool TryGetSelectable(out Selectable component)
        => TryGetSelectable(out component);
        public bool TryGetSelectable<T>(out T component) where T : Component
        => TryGetSelectable(out component);

        public bool IsHolding
        => Selectable != null && Selectable.IsHeldBy(this);
        public void TryPress()
        {
            if (Selectable != null
            && !Selectable.IsHeld)
                Selectable.GetPressedBy(this);
        }
        public void TryRelease(bool fullClick)
        {
            if (Selectable != null
            && Selectable.IsHeldBy(this))
                Selectable.GetReleasedBy(this, fullClick);
        }
        public void TryFullClick()
        {
            if (Selectable != null
            && !Selectable.IsHeld)
            {
                Selectable.GetPressedBy(this);
                Selectable.GetReleasedBy(this, true);
            }
        }
    }
}
namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Selecter : AUserOfOne<Selecter, Selectable>
    {
        // Events
        public AutoEvent<Selectable, Selectable> OnChangeSelectable
        => OnChangeUser;
        public readonly AutoEvent OnPress = new();
        public readonly AutoEvent OnRelease = new();

        // Publics
        public Selectable Selectable
        {
            get => _usable;
            set
            {
                if (value == _usable
                || value != null && !PerformAllTests(this, value))
                    return;

                if (IsPressing && Selectable.PressedDeselectBehavior == PressedDeselectBehavior.ReleaseAndDeselect)
                    Selectable.GetReleasedBy(this, false);

                if (!IsPressing || Selectable.PressedDeselectBehavior != PressedDeselectBehavior.KeepPressedAndSelected)
                    SetUsable(value);
            }
        }
        public bool IsSelectingAny
        => IsUsingAny;
        public bool IsSelecting(Selectable selectable)
        => IsUsing(selectable);
        public bool IsSelecting<T>() where T : Component
        => IsSelecting<T>();
        public bool TryGetSelectable(out Selectable component)
        => TryGetSelectable(out component);
        public bool TryGetSelectable<T>(out T component) where T : Component
        => TryGetSelectable(out component);

        public bool IsPressing
        => Selectable != null && Selectable.IsPressedBy(this);
        public void Press()
        {
            OnPress.Invoke();
            if (Selectable == null
            || Selectable.IsPressedBy(this))
                return;

            Selectable.GetPressedBy(this);
        }
        public void Release(bool click)
        {
            OnRelease.Invoke();
            if (!IsPressing)
                return;

            Selectable.GetReleasedBy(this, click);
        }
        public void Click()
        {
            Press();
            Release(true);
        }
    }
}
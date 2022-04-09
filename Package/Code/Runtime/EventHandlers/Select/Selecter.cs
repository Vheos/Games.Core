namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.General;
    using System.Collections.Generic;

    [DisallowMultipleComponent]
    sealed public class Selecter : AUserOfOne<Selecter, Selectable>
    {
        // Events
        public AutoEvent<Selecter> OnChangeSelectable
        => OnChangeUsable;
        public readonly AutoEvent<Selecter> OnPress = new();
        public readonly AutoEvent<Selecter> OnRelease = new();

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
        => TryGetUsable(out component);
        public bool TryGetSelectable<T>(out T component) where T : Component
        => TryGetUsable(out component);

        public bool IsPressing
        => Selectable != null && Selectable.IsPressedBy(this);
        public void Press()
        {
            OnPress.Invoke(this);

            if (Selectable == null
            || Selectable.IsPressedBy(this)
            || !CanUse(Selectable)
            || !CanPress(Selectable))
                return;

            Selectable.GetPressedBy(this);
        }
        public void Release(bool click)
        {
            OnRelease.Invoke(this);

            if (!IsPressing)
                return;

            Selectable.GetReleasedBy(this, click);
        }
        public void Click()
        {
            Press();
            Release(true);
        }
        public void AddPressTest(Func<Selectable, bool> test)
        => _pressTests.Add(test);
        public void RemovePressTest(Func<Selectable, bool> test)
        => _pressTests.Remove(test);

        // Private
        private HashSet<Func<Selectable, bool>> _pressTests = new();
        private bool CanPress(Selectable selectable)
        {
            foreach (var test in _pressTests)
                if (!test(selectable))
                    return false;
            return true;
        }
    }
}
namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Selecter : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Selectable, Selectable> OnChangeSelectable = new();

        // Publics
        public Selectable Selectable
        {
            get => _selectable;
            set
            {
                if (value == _selectable
                || value != null && (!value.isActiveAndEnabled || value.IsSelectedBy(this) || !value.CanGetSelectedBy(this)))
                    return;

                Selectable previousSelectable = _selectable;
                if (previousSelectable != null
                && previousSelectable.IsSelectedBy(this))
                    previousSelectable.GetUnselectedBy(this);

                _selectable = value;
                if (_selectable != null)
                    _selectable.GetSelectedBy(this);

                OnChangeSelectable.Invoke(previousSelectable, _selectable);
            }
        }
        public bool TryGetSelectable<T>(out T component) where T : Component
        {
            if (_selectable != null
            && _selectable.TryGet(out component))
                return true;

            component = default;
            return false;
        }
        public bool IsSelectingAny
        => _selectable != null;
        public bool IsSelecting(Selectable selectable)
        => _selectable == selectable;
        public bool IsSelecting<T>() where T : Component
        => _selectable != null && _selectable.Has<T>();
        public bool IsHolding
        => _selectable != null && _selectable.IsHeldBy(this);
        public void TryPress()
        {
            if (_selectable != null
            && !_selectable.IsHeld)
                _selectable.GetPressedBy(this);
        }
        public void TryRelease(bool fullClick)
        {
            if (_selectable != null
            && _selectable.IsHeldBy(this))
                _selectable.GetReleasedBy(this, fullClick);
        }
        public void TryFullClick()
        {
            if (_selectable != null
            && !_selectable.IsHeld)
            {
                _selectable.GetPressedBy(this);
                _selectable.GetReleasedBy(this, true);
            }
        }

        // Privates
        private Selectable _selectable;
    }
}
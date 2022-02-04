namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(Updatable))]
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
                if (value != null && !value.CanGetSelectedBy(this)
                || value == null && _selectable == null)
                    return;

                Selectable previousSelectable = _selectable;
                if (previousSelectable != null
                && previousSelectable.CanGetUnselectedBy(this))
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
            if (Selectable != null
            && Selectable.CanGetPressed)
                Selectable.GetPressedBy(this);
        }
        public void TryRelease(bool fullClick)
        {
            if (Selectable != null
            && Selectable.CanGetReleasedBy(this))
                Selectable.GetReleasedBy(this, fullClick);
        }
        public void TryFullClick()
        {
            if (Selectable != null
            && Selectable.CanGetPressed)
            {
                Selectable.GetPressedBy(this);
                Selectable.GetReleasedBy(this, true);
            }
        }

        // Privates
        private Selectable _selectable;
        private void Updatable_OnUpdate()
        {
            if (Selectable != null
            && Selectable.CanGetHeldBy(this))
                Selectable.GetHeldBy(this);
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            Get<Updatable>().OnUpdate.SubEnableDisable(this, Updatable_OnUpdate);
        }
    }
}
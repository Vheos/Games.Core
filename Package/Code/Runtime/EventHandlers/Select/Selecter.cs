namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Vheos.Tools.Extensions.General;

    [RequireComponent(typeof(Updatable))]
    [DisallowMultipleComponent]
    sealed public class Selecter : ABaseComponent
    {
        // Publics
        public Selectable Selectable
        {
            get => _selectable;
            set
            {
                Selectable previousTarget = _selectable;
                _selectable = value;

                if (previousTarget != _selectable)
                {
                    if (previousTarget != null)
                        previousTarget.TryLoseSelectionFrom(this);
                    if (_selectable != null)
                        _selectable.TryGainSelectionFrom(this);
                }
            }
        }
        public bool IsSelectingAny
        => _selectable != null;
        public bool IsSelecting(Selectable selectable)
        => _selectable == selectable;
        public bool IsHolding
        => _selectable != null && _selectable.IsHeldBy(this);
        public void TryPress()
        {
            if (Selectable != null)
                Selectable.TryGetPressedBy(this);
        }
        public void TryRelease(bool fullClick)
        {
            if (Selectable != null)
                Selectable.TryGetReleasedBy(this, fullClick);
        }
        public void TryFullClick()
        {
            if (Selectable != null)
            {
                Selectable.TryGetPressedBy(this);
                Selectable.TryGetReleasedBy(this, true);
            }
        }

        // Privates
        private Selectable _selectable;
        private void Updatable_OnUpdate()
        {
            if (Selectable != null)
                Selectable.TryGetHeldBy(this);
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            Get<Updatable>().OnUpdate.SubscribeAuto(this, Updatable_OnUpdate);
        }
    }
}
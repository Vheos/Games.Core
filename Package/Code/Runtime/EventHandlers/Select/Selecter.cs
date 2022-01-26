namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Vheos.Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Selecter : ABaseComponent
    {
        // Publics
        private Selectable _selectable;
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
        public void Press()
        {
            if (Selectable != null)
                Selectable.TryGetPressed(this);
        }
        public void Release(bool fullClick)
        {
            if (Selectable != null)
                Selectable.TryGetReleased(this, fullClick);
        }
        public void FullClick()
        {
            if (Selectable != null)
            {
                Selectable.TryGetPressed(this);
                Selectable.TryGetReleased(this, true);
            }
        }
    }
}
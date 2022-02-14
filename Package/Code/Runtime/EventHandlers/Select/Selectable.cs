namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Vheos.Tools.Extensions.General;

    [DisallowMultipleComponent]
    public class Selectable : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Selecter, bool> OnGainSelection = new();
        public readonly AutoEvent<Selecter, bool> OnLoseSelection = new();
        public readonly AutoEvent<Selecter> OnPress = new();
        public readonly AutoEvent<Selecter> OnHold = new();
        public readonly AutoEvent<Selecter, bool> OnRelease = new();

        // Publics
        public IReadOnlyCollection<Selecter> Selecters
        => _selecters;
        public Selecter Holder
        { get; private set; }
        public bool IsSelected
        => _selecters.Count > 0;
        public bool IsSelectedBy(Selecter selecter)
        => _selecters.Contains(selecter);
        public bool IsHeld
        => Holder != null;
        public bool IsHeldBy(Selecter selecter)
        => ReferenceEquals(Holder, selecter);
        public void ClearSelectionAndHolder()
        {
            if (IsHeld)
            {
                Selecter previousHolder = Holder;
                Holder = null;
                OnRelease.Invoke(previousHolder, false);
            }

            if (IsSelected)
                foreach (var selecter in  _selecters.MakeCopy())
                {
                    _selecters.Remove(selecter);
                    OnLoseSelection.Invoke(selecter, _selecters.Count == 0);
                }
        }

        // Internals
        internal bool CanGetSelectedBy(Selecter selecter)
        => isActiveAndEnabled && !IsSelectedBy(selecter);
        internal bool CanGetUnselectedBy(Selecter selecter)
        => isActiveAndEnabled && IsSelectedBy(selecter);
        internal bool CanGetPressed
        => isActiveAndEnabled && !IsHeld;
        internal bool CanGetReleasedBy(Selecter selecter)
        => isActiveAndEnabled && IsHeldBy(selecter);
        internal bool CanGetHeldBy(Selecter selecter)
        => isActiveAndEnabled && IsHeldBy(selecter);
        internal void GetSelectedBy(Selecter selecter)
        {
            _selecters.Add(selecter);
            OnGainSelection.Invoke(selecter, _selecters.Count == 1);   // is first
        }
        internal void GetUnselectedBy(Selecter selecter)
        {
            _selecters.Remove(selecter);
            OnLoseSelection.Invoke(selecter, _selecters.Count == 0);   // was last
        }
        internal void GetPressedBy(Selecter selecter)
        {
            Holder = selecter;
            OnPress.Invoke(selecter);
        }
        internal void GetReleasedBy(Selecter selecter, bool withinTrigger)
        {
            Holder = null;
            OnRelease.Invoke(selecter, withinTrigger);
        }
        internal void GetHeldBy(Selecter selecter)
        => OnHold.Invoke(selecter);

        // Privates
        private readonly HashSet<Selecter> _selecters = new();

        // Play
        protected override void PlayDisable()
        {
            base.PlayDisable();
            ClearSelectionAndHolder();
        }
    }
}
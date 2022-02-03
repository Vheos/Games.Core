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
        internal bool TryGainSelectionFrom(Selecter selecter)
        {
            if (!enabled || _selecters.Contains(selecter))
                return false;

            _selecters.Add(selecter);
            OnGainSelection.Invoke(selecter, _selecters.Count == 1);   // is first
            return true;
        }
        internal bool TryLoseSelectionFrom(Selecter selecter)
        {
            if (!enabled || !_selecters.Contains(selecter))
                return false;

            _selecters.Remove(selecter);
            OnLoseSelection.Invoke(selecter, _selecters.Count == 0);   // was last
            return true;
        }
        internal bool TryGetPressedBy(Selecter selecter)
        {
            if (!enabled || IsHeld)
                return false;

            Holder = selecter;
            OnPress.Invoke(selecter);
            return true;
        }
        internal bool TryGetReleasedBy(Selecter selecter, bool withinTrigger)
        {
            if (!enabled || !IsHeldBy(selecter))
                return false;

            Holder = null;
            OnRelease.Invoke(selecter, withinTrigger);
            return true;
        }
        internal bool TryGetHeldBy(Selecter selecter)
        {
            if (!enabled || !IsHeldBy(selecter))
                return false;

            OnHold.Invoke(selecter);
            return true;
        }

        // Publics
        public IReadOnlyCollection<Selecter> Selecters
        => _selecters;
        public Selecter Holder
        { get; private set; }
        public bool IsSelected
        => _selecters.Count > 0;
        public bool IsHeld
        => Holder != null;
        public bool IsHeldBy(Selecter selecter)
        => ReferenceEquals(Holder, selecter);
        public void ClearSelectionAndHolder()
        {
            if (IsSelected)
                foreach (var selecter in _selecters.MakeCopy())
                {
                    _selecters.Remove(selecter);
                    OnLoseSelection.Invoke(selecter, _selecters.Count == 0);
                }

            if (IsHeld)
            {
                Selecter previousHolder = Holder;
                Holder = null;
                OnRelease.Invoke(previousHolder, false);
            }
        }

        // Privates
        private HashSet<Selecter> _selecters;

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _selecters = new HashSet<Selecter>();
        }
        protected override void PlayDisable()
        {
            base.PlayDisable();
            ClearSelectionAndHolder();
        }
    }
}
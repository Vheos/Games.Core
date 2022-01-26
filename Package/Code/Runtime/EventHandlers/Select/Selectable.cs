namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;

    [RequireComponent(typeof(Collider))]
    [DisallowMultipleComponent]
    public class Selectable : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Selecter, bool> OnGainSelection = new();
        public readonly AutoEvent<Selecter, bool> OnLoseSelection = new();
        public readonly AutoEvent<Selecter> OnPress = new();
        public readonly AutoEvent<Selecter> OnHold = new();
        public readonly AutoEvent<Selecter, bool> OnRelease = new();
        internal void TryGainSelectionFrom(Selecter selecter)
        {
            if (_selecters.TryAddUnique(selecter))
                OnGainSelection.Invoke(selecter, _selecters.Count == 1);   // is first
        }
        internal void TryLoseSelectionFrom(Selecter selecter)
        {
            if (_selecters.Remove(selecter))
                OnLoseSelection.Invoke(selecter, _selecters.Count == 0);   // was last
        }
        internal void TryGetPressed(Selecter selecter)
        {
            if (IsHeld)
                return;

            Holder = selecter;
            OnPress.Invoke(selecter);
        }
        internal void TryGetReleased(Selecter selecter, bool withinTrigger)
        {
            if (!IsHeldBy(selecter))
                return;

            Holder = null;
            OnRelease.Invoke(selecter, withinTrigger);
        }
        internal bool TryGetHeld(Selecter selecter)
        {
            if (!IsHeldBy(selecter))
                return false;

            OnHold.Invoke(selecter);
            return true;
        }

        // Publics
        public IReadOnlyCollection<Selecter> Selecters
        => _selecters;
        public Selecter Holder
        { get; private set; }
        public bool IsHeld
        => Holder != null;
        public bool IsHeldBy(Selecter selecter)
        => ReferenceEquals(Holder, selecter);

        // Privates
        private HashSet<Selecter> _selecters;

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _selecters = new HashSet<Selecter>();
        }
    }
}
namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Vheos.Tools.Extensions.General;

    [DisallowMultipleComponent]
    public class Selectable : AMultiReceptor<Selectable, Selecter>
    {
        // Events
        public AutoEvent<Selecter, bool> OnGainSelection
        => OnGainEffect;
        public AutoEvent<Selecter, bool> OnLoseSelection
        => OnLoseEffect;
        public readonly AutoEvent<Selecter> OnPress = new();
        public readonly AutoEvent<Selecter, bool> OnRelease = new();

        // Publics (redirects)
        public IReadOnlyCollection<Selecter> Selecters
        => _effectors;
        public bool IsSelected
        => IsEffected;
        public bool IsSelectedBy(Selecter selecter)
        => IsEffectedBy(selecter);

        public Selecter Holder
        { get; private set; }
        public bool IsHeld
        => Holder != null;
        public bool IsHeldBy(Selecter selecter)
        => Holder == selecter;

        // Internals
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
    }
}
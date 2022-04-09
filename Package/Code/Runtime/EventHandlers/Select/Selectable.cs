namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Vheos.Tools.Extensions.General;

    [DisallowMultipleComponent]
    public class Selectable : AUsableByMany<Selectable, Selecter>
    {
        // Inspector
        [field: SerializeField] public PressedDeselectBehavior PressedDeselectBehavior { get; set; }

        // Events
        /// <summary>
        /// <c><see cref="Selectable"/></c> - the component that raised this event<br/>
        /// <c><see cref="Selecter"/></c> - the component that selected this selectable<br/>
        /// </summary>
        public AutoEvent<Selectable, Selecter> OnGetSelected
        => OnStartBeingUsed;
        /// <summary>
        /// <c><see cref="Selectable"/></c> - the component that raised this event<br/>
        /// <c><see cref="Selecter"/></c> - the component that deselected this selectable<br/>
        /// </summary>
        public AutoEvent<Selectable, Selecter> OnGetDeselected
        => OnStopBeingUsed;
        /// <summary>
        /// <c><see cref="Selectable"/></c> - the component that raised this event<br/>
        /// <c><see cref="Selecter"/></c> - the component that pressed this selectable
        /// </summary>
        public readonly AutoEvent<Selectable, Selecter> OnGetPressed = new();
        /// <summary>
        /// <c><see cref="Selectable"/></c> - the component that raised this event<br/>
        /// <c><see cref="Selecter"/></c> - the component that released this selectable<br/>
        /// <c><see cref="bool"/></c> - whether the release also counts as a full click
        /// </summary>
        public readonly AutoEvent<Selectable, Selecter, bool> OnGetReleased = new();

        // Publics
        public IReadOnlyCollection<Selecter> Selecters
        => _users;
        public bool IsSelected
        => IsBeingUsed;
        public bool IsSelectedBy(Selecter selecter)
        => IsBeingUsedBy(selecter);
        public bool IsSelectedByMany
        => IsBeingUsedByMany;

        public Selecter Presser
        { get; private set; }
        public bool IsPressed
        => Presser != null;
        public bool IsPressedBy(Selecter selecter)
        => Presser == selecter;

        // Internals
        internal void GetPressedBy(Selecter selecter)
        {
            Presser = selecter;
            OnGetPressed.Invoke(this, selecter);
        }
        internal void GetReleasedBy(Selecter selecter, bool click)
        {
            Presser = null;
            OnGetReleased.Invoke(this, selecter, click);
        }
    }
}
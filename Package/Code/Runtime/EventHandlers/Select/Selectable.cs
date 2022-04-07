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
        [field: SerializeField] public PressedDeselectBehavior PressedDeselectBehavior { get; private set; }

        // Events
        /// <summary>
        /// <c><see cref="Selecter"/></c> - the component that selected this selectable<br/>
        /// <c><see cref="bool"/></c> - whether the above component is the only one selecting this selectable
        /// </summary>
        public readonly AutoEvent<Selecter, bool> OnGetSelected = new();
        /// <summary>
        /// <c><see cref="Selecter"/></c> - the component that deselected this selectable<br/>
        /// <c><see cref="bool"/></c> - whether the above component was the only one selecting this selectable
        /// </summary>
        public readonly AutoEvent<Selecter, bool> OnGetDeselected = new();
        /// <summary> <c><see cref="Selecter"/></c> - the component that pressed this selectable<br/> </summary>
        public readonly AutoEvent<Selecter> OnGetPressed = new();
        /// <summary>
        /// <c><see cref="Selecter"/></c> - the component that released this selectable<br/>
        /// <c><see cref="bool"/></c> - whether the release also counts as a full click
        /// </summary>
        public readonly AutoEvent<Selecter, bool> OnGetReleased = new();

        // Publics
        public IReadOnlyCollection<Selecter> Selecters
        => _users;
        public bool IsSelected
        => IsBeingUsed;
        public bool IsSelectedBy(Selecter selecter)
        => IsBeingUsedBy(selecter);

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
            OnGetPressed.Invoke(selecter);
        }
        internal void GetReleasedBy(Selecter selecter, bool click)
        {
            Presser = null;
            OnGetReleased.Invoke(selecter, click);
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            OnStartBeingUsed.SubEnableDisable(this, user => OnGetSelected.Invoke(user, _users.Count == 1));
            OnStopBeingUsed.SubEnableDisable(this, user => OnGetDeselected.Invoke(user, _users.Count == 0));
        }
    }
}
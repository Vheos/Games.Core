namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.UnityObjects;

    abstract public class AComponentGroup<T> where T : Component
    {
        // Events
        public AutoEvent OnMembersChanged
        { get; } = new AutoEvent();

        // Publics
        public IReadOnlyCollection<T> Members
        => _members;
        public int Count
        => _members.Count;
        public Vector3 Midpoint
        => _members.Midpoint();
        virtual public void TryAddMember(T member)
        {
            if (_members.TryAddUnique(member))
                OnMembersChanged?.Invoke();
        }
        virtual public void TryRemoveMember(T member)
        {
            if (_members.Remove(member))
                OnMembersChanged?.Invoke();
        }

        // Privates
        protected HashSet<T> _members;

        // Initializers
        protected AComponentGroup()
        => _members = new HashSet<T>();
    }
}
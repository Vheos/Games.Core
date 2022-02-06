namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Equipable : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Equiper> OnGetEquiped = new();
        public readonly AutoEvent<Equiper> OnGetUnequiped = new();

        // Getters
        public readonly Getter<int> EquipSlot = new();

        // Publics
        public Equiper Equiper
        { get; private set; }
        public bool IsEquipped
        => Equiper != null;
        public bool IsEquippedBy(Equiper equiper)
        => Equiper != null && Equiper == equiper;

        // Internals
        internal bool CanGetEquipped
        => isActiveAndEnabled && !IsEquipped;
        internal bool CanGetUnequippedBy(Equiper equiper)
        => isActiveAndEnabled && IsEquippedBy(equiper);
        internal void GetEquippedBy(Equiper equiper)
        {
            Equiper = equiper;
            OnGetEquiped.Invoke(Equiper);
        }
        internal void GetUnequipped()
        {
            var previousEquiper = Equiper;
            Equiper = null;
            OnGetUnequiped.Invoke(previousEquiper);
        }
    }
}
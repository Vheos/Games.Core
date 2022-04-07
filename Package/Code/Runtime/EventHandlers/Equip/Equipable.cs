namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Equipable : AUsableByOne<Equipable, Equiper>
    {
        // Events
        public AutoEvent<Equiper> OnGetEquiped => OnStartBeingUsed;
        public AutoEvent<Equiper> OnGetUnequiped => OnStopBeingUsed;

        // Getters
        public readonly Getter<int> EquipSlot = new();

        // Publics
        public Equiper EquiperNEW
        => _user;
        public bool IsEquipped
        => IsBeingUsed;
        public bool IsEquippedBy(Equiper equiper)
        => IsBeingUsedBy(equiper);

        // Internals
        internal void GetEquippedBy(Equiper equiper)
        => StartBeingUsedBy(equiper);
        internal void GetUnequipped()
        => StopBeingUsed();
    }
}
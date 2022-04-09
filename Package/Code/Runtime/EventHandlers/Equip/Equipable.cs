namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Equipable : AUsableByOne<Equipable, Equiper>
    {
        // Inspector
        [field: SerializeField] public int EquipSlot { get; set; }

        // Events
        public AutoEvent<Equipable, Equiper> OnGetEquiped
        => OnStartBeingUsed;
        public AutoEvent<Equipable, Equiper> OnGetUnequiped
        => OnStopBeingUsed;

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
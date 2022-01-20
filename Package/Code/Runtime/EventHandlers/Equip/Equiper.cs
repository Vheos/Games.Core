namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    [DisallowMultipleComponent]
    sealed public class Equiper : ABaseComponent
    {
        // Events
        public AutoEvent<int, Equipable, Equipable> OnChangeEquipable
        { get; } = new AutoEvent<int, Equipable, Equipable>();

        // Getters
        public Getter<int, Transform> AttachTransformsBySlot
        { get; } = new Getter<int, Transform>();

        // Publics
        public IReadOnlyDictionary<int, Equipable> EquipablesBySlot
        => _equipablesBySlot;
        public void TryEquip(Equipable equipable)
        {
            if (equipable == null || HasEquiped(equipable))
                return;

            int slot = equipable.EquipSlot;
            TryGetEquiped(slot, out var previousEquipable);
            if (previousEquipable != null)
                RemoveEquipable(previousEquipable);

            AddEquipable(equipable);
            OnChangeEquipable?.Invoke(slot, previousEquipable, equipable);
        }
        public void TryUnequip(int slot)
        {
            if (!HasEquiped(slot))
                return;

            Equipable equipable = _equipablesBySlot[slot];
            RemoveEquipable(equipable);
            OnChangeEquipable?.Invoke(slot, equipable, null);
        }
        public void TryUnequip(Equipable equipable)
        {
            if (!HasEquiped(equipable))
                return;

            RemoveEquipable(equipable);
            OnChangeEquipable?.Invoke(equipable.EquipSlot, equipable, null);
        }
        public bool HasEquiped(int slot)
        => _equipablesBySlot.ContainsKey(slot);
        public bool HasEquiped(Equipable equipable)
        => TryGetEquiped(equipable.EquipSlot, out var equiped) && equipable == equiped;
        public Equipable GetEquiped(int slot)
        => _equipablesBySlot[slot];
        public bool TryGetEquiped(int slot, out Equipable equipable)
        => _equipablesBySlot.TryGetValue(slot, out equipable);

        // Privates
        private readonly Dictionary<int, Equipable> _equipablesBySlot = new Dictionary<int, Equipable>();
        private void AddEquipable(Equipable equipable)
        {
            _equipablesBySlot.Add(equipable.EquipSlot, equipable);
            equipable.Equiper = this;
        }
        private void RemoveEquipable(Equipable equipable)
        {
            _equipablesBySlot.Remove(equipable.EquipSlot);
            equipable.Equiper = null;
        }
    }
}
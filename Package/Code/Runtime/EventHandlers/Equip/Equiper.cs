namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Equiper : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Equipable, Equipable> OnChangeEquipable = new();

        // Publics
        public IReadOnlyDictionary<int, Equipable> EquipablesBySlot
        => _equipablesBySlot;
        public Equipable GetEquipable(int slot)
        => _equipablesBySlot[slot];
        public T GetEquipable<T>() where T : Component
        {
            foreach (var equipableBySlot in _equipablesBySlot)
                if (equipableBySlot.Value.TryGet(out T component))
                    return component;

            return null;
        }
        public bool TryGetEquipable(int slot, out Equipable equipable)
        => _equipablesBySlot.TryGetValue(slot, out equipable);
        public bool TryGetEquipable<T>(out T component) where T : Component
        {
            foreach (var equipableBySlot in _equipablesBySlot)
                if (equipableBySlot.Value.TryGet(out component))
                    return true;

            component = null;
            return false;
        }
        public bool HasEquipped(int slot)
        => _equipablesBySlot.ContainsKey(slot);
        public bool HasEquipped<T>() where T : Component
        => _equipablesBySlot.Any(t => t.Value.Has<T>());
        public bool HasEquipped(Equipable equipable)
        => _equipablesBySlot.Any(t => t.Value == equipable);
        public bool TryUnequip(int slot)
        {
            if (!TryGetEquipable(slot, out var equipable))
                return false;

            Unequip(equipable, true);
            return true;
        }
        public bool TryUnequip<T>() where T : Component
        {
            if (!TryGetEquipable<T>(out var component))
                return false;

            Unequip(component.GetComponent<Equipable>(), true);
            return true;
        }
        public bool TryUnequip(Equipable equipable)
        {
            if (!HasEquipped(equipable))
                return false;

            Unequip(equipable, true);
            return true;
        }
        public bool TryEquip(Equipable equipable)
        {
            if (equipable == null
            || !equipable.CanGetEquipped)
                return false;

            if (TryGetEquipable(equipable.EquipSlot, out var previousEquipable)
            && previousEquipable.CanGetUnequippedBy(this))
                Unequip(previousEquipable);

            _equipablesBySlot.Add(equipable.EquipSlot, equipable);
            equipable.GetEquippedBy(this);

            OnChangeEquipable?.Invoke(previousEquipable, equipable);
            return true;
        }

        // Privates
        private readonly Dictionary<int, Equipable> _equipablesBySlot = new();
        private void Unequip(Equipable equipable, bool callOnChangeEquipable = false)
        {
            _equipablesBySlot.Remove(equipable.EquipSlot);
            equipable.GetUnequipped();
            if (callOnChangeEquipable)
                OnChangeEquipable?.Invoke(equipable, null);
        }
    }
}
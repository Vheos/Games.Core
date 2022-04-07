namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Vheos.Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Equiper : AUserOfMany<Equiper, Equipable>
    {
        // Events
        public readonly AutoEvent<Equipable, Equipable> OnChangeEquipable = new();

        // Publics
        public IReadOnlyCollection<Equipable> Equipables
        => _usables;
        public Equipable GetEquipable(int slot)
        => _usables.FirstOrDefault(t => t.EquipSlot == slot);
        public T GetEquipable<T>() where T : Component
        {
            foreach (var equipable in _usables)
                if (equipable.TryGet(out T component))
                    return component;
            return null;
        }
        public bool TryGetEquipable(int slot, out Equipable equipable)
        => GetEquipable(slot).TryNonNull(out equipable);
        public bool TryGetEquipable<T>(out T component) where T : Component
        => GetEquipable<T>().TryNonNull(out component);

        public bool HasEquipped(int slot)
        => _usables.Any(t => t.EquipSlot == slot);
        public bool HasEquipped<T>() where T : Component
        => _usables.Any(t => t.Has<T>());
        public bool HasEquipped(Equipable equipable)
        => _usables.Contains(equipable);

        public bool TryEquip(Equipable equipable)
        {
            if (equipable == null
            || _usables.Contains(equipable)
            || !PerformAllTests(this, equipable))
                return false;

            if (TryGetEquipable(equipable.EquipSlot, out var previousEquipable))
                TryRemoveUsable(previousEquipable);

            TryAddUsable(equipable);
            OnChangeEquipable.Invoke(previousEquipable, equipable);

            return true;
        }
        public bool TryUnequip(int slot)
        => TryGetEquipable(slot, out var equipable)
        && TryRemoveUsable(equipable);
        public bool TryUnequip<T>() where T : Component
        => TryGetEquipable<T>(out var equipable)
        && TryRemoveUsable(equipable.GetComponent<Equipable>());
        public bool TryUnequip(Equipable equipable)
        {
            if (!TryRemoveUsable(equipable))
                return false;

            OnChangeEquipable.Invoke(equipable, null);
            return true;
        }
    }
}
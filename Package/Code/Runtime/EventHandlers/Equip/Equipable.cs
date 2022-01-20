namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    
    [DisallowMultipleComponent]
    sealed public class Equipable : ABaseComponent
    {
        // Events
        public AutoEvent<Equiper, Equiper> OnChangeEquiper
        { get; } = new AutoEvent<Equiper, Equiper>();

        // Getters
        public Getter<int> EquipSlot
        { get; } = new Getter<int>();

        // Publics
        public Equiper Equiper
        {
            get => _equiper;
            internal set
            {
                Equiper previousEquiper = _equiper;
                _equiper = value;

                if (previousEquiper != _equiper)
                    OnChangeEquiper?.Invoke(previousEquiper, _equiper);
            }
        }

        // Privates
        private Equiper _equiper;
    }
}
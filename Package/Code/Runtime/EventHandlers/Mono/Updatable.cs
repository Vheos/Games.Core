namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Updatable : ABaseComponent
    {
        // Events
        public readonly AutoEvent OnUpdate = new();
        public readonly AutoEvent OnUpdateLate = new();
        public readonly AutoEvent OnUpdateFixed = new();

        // Play
#pragma warning disable IDE0051 // Remove unused private members
        private void Update()
        => OnUpdate.Invoke();
        private void LateUpdate()
        => OnUpdateLate.Invoke();
        private void FixedUpdate()
        => OnUpdateFixed.Invoke();
    }
}
namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Updatable : ABaseComponent
    {
        // Events
        public AutoEvent OnUpdate
        { get; } = new AutoEvent();
        public AutoEvent OnUpdateLate
        { get; } = new AutoEvent();
        public AutoEvent OnUpdateFixed
        { get; } = new AutoEvent();

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
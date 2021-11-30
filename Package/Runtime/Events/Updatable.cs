namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    [UnityEngine.DisallowMultipleComponent]
    sealed public class Updatable : ABaseComponent
    {
        // Events
        public Event OnUpdate
        { get; } = new Event();
        public Event OnUpdateLate
        { get; } = new Event();
        public Event OnUpdateFixed
        { get; } = new Event();

        // Mono
        private void Update()
        => OnUpdate?.Invoke();
        private void LateUpdate()
        => OnUpdateLate?.Invoke();
        private void FixedUpdate()
        => OnUpdateFixed?.Invoke();
    }
}
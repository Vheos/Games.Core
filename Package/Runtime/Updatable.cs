namespace Vheos.Tools.UnityCore
{
    [UnityEngine.DisallowMultipleComponent]
    sealed public class Updatable : ABaseComponent
    {
        // Events
        public Event OnUpdated
        { get; } = new Event();
        public Event OnUpdatedLate
        { get; } = new Event();
        public Event OnUpdatedFixed
        { get; } = new Event();

        // Mono
        private void Update()
        => OnUpdated?.Invoke();
        private void LateUpdate()
        => OnUpdatedLate?.Invoke();
        private void FixedUpdate()
        => OnUpdatedFixed?.Invoke();
    }
}
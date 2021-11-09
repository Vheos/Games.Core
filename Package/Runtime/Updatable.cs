/*
namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Updatable : APlayable
    {
        // Events
        public event Action OnPlayUpdate;
        public event Action OnPlayUpdateLate;
        public event Action OnPlayUpdateFixed;

        // Play
        private void Update()
        => OnPlayUpdate?.Invoke();
        private void LateUpdate()
        => OnPlayUpdateLate?.Invoke();
        private void FixedUpdate()
        => OnPlayUpdateFixed?.Invoke();

#if UNITY_EDITOR
        // Debug
        [ContextMenu(nameof(DisplayDebugInfo))]
        public void DisplayDebugInfo()
        {
            (Action Event, string Name)[] eventsByName =
            {
                (OnPlayUpdate, nameof(OnPlayUpdate)),
                (OnPlayUpdateLate, nameof(OnPlayUpdateLate)),
                (OnPlayUpdateFixed, nameof(OnPlayUpdateFixed)),
            };

            foreach (var eventByName in eventsByName)
            {
                Delegate[] callList = eventByName.Event != null ? eventByName.Event.GetInvocationList() : new Delegate[0];
                Debug.Log($"{eventByName.Name} ({callList.Length})");
                foreach (var call in callList)
                    Debug.Log($"\t- { call.Target}");
                Debug.Log("");
            }
        }
#endif
    }
}
*/
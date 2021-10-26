namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.UtilityN;
    using Tools.Extensions.General;

    [DefaultExecutionOrder(int.MinValue)]
    [DisallowMultipleComponent]
    sealed public class UpdateManager : MonoBehaviour
    {
        // Events
        static internal event Action OnUpdate;
        static internal event Action OnUpdateLate;
        static internal event Action OnUpdateFixed;

        // Initializers
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static private void StaticInitialize()
        {
            OnUpdate = null;
            OnUpdateLate = null;
            OnUpdateFixed = null;
        }

        // Mono
        private void Update()
        => OnUpdate?.Invoke();
        private void LateUpdate()
        => OnUpdateLate?.Invoke();
        private void FixedUpdate()
        => OnUpdateFixed?.Invoke();

#if UNITY_EDITOR
        // Debug
        [ContextMenu("Display Debug Info")]
        public void DisplayDebugInfo()
        {
            (Action Event, string Name)[] eventsByName =
            {
                (OnUpdate, nameof(OnUpdate)),
                (OnUpdateLate, nameof(OnUpdateLate)),
                (OnUpdateFixed, nameof(OnUpdateFixed)),
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
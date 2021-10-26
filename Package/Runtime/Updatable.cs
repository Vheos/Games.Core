namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    [DefaultExecutionOrder(int.MaxValue)]
    [DisallowMultipleComponent]
    sealed public class Updatable : APlayable
    {
        // Events
        public event Action OnPlayUpdate;
        public event Action OnPlayUpdateLate;
        public event Action OnPlayUpdateFixed;
        internal void PlayUpdate()
        => OnPlayUpdate?.Invoke();
        internal void PlayUpdateLate()
        => OnPlayUpdateLate?.Invoke();
        internal void PlayUpdateFixed()
        => OnPlayUpdateFixed?.Invoke();

        // Privates
        private void SubscribeToManager()
        {
            if (OnPlayUpdate != null)
                UpdateManager.OnUpdate += PlayUpdate;
            if (OnPlayUpdateLate != null)
                UpdateManager.OnUpdateLate += PlayUpdateLate;
            if (OnPlayUpdateFixed != null)
                UpdateManager.OnUpdateFixed += PlayUpdateFixed;
        }
        private void UnsubscribeFromManager()
        {
            UpdateManager.OnUpdate -= PlayUpdate;
            UpdateManager.OnUpdateLate -= PlayUpdateLate;
            UpdateManager.OnUpdateFixed -= PlayUpdateFixed;
        }

        // Play
        override public void PlayEnable()
        {
            base.PlayEnable();
            SubscribeToManager();
        }
        override public void PlayDisable()
        {
            base.PlayDisable();
            UnsubscribeFromManager();
        }

#if UNITY_EDITOR
        // Debug
        [ContextMenu("Display Debug Info")]
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
namespace Vheos.Games.Core
{
    using UnityEngine;
    /// <summary> Wraps and virtualizes common <c><see cref="MonoBehaviour"/></c> events </summary>
    public class Playable : MonoBehaviour
    {
        // Events
        public readonly AutoEvent OnPlayEnable = new();
        public readonly AutoEvent OnPlayStart = new();
        public readonly AutoEvent OnPlayDisable = new();
        public readonly AutoEvent OnPlayDestroy = new();

        // Protected
        /// <summary> Wraps <c><see langword="Awake"/></c>() </summary>
        virtual protected void PlayAwake()
        { }
        /// <summary> Wraps <c><see langword="OnEnable"/></c>() </summary>
        virtual protected void PlayEnable()
        { }
        /// <summary> Wraps <c><see langword="Start"/></c>() </summary>
        virtual protected void PlayStart()
        { }
        /// <summary> Wraps <c><see langword="OnDisable"/></c>() </summary>
        virtual protected void PlayDisable()
        { }
        /// <summary> Wraps <c><see langword="OnDestroy"/></c>() </summary>
        virtual protected void PlayDestroy()
        { }

        // Play
#pragma warning disable IDE0051 // Remove unused private members
        private void Awake()
        {
            PlayAwake();
        }
        private void OnEnable()
        {
            PlayEnable();
            OnPlayEnable.Invoke();
        }
        private void Start()
        {
            PlayStart();
            OnPlayStart.Invoke();
        }
        private void OnDisable()
        {
            PlayDisable();
            OnPlayDisable.Invoke();
        }
        private void OnDestroy()
        {
            PlayDestroy();
            OnPlayDestroy.Invoke();
        }
    }
}
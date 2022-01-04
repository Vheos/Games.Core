namespace Vheos.Tools.UnityCore
{
    using UnityEngine;
    /// <summary> Wraps and virtualizes common <c><see cref="MonoBehaviour"/></c> events </summary>
    abstract public class APlayable : MonoBehaviour
    {
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

        // Internals
        virtual protected private void PlayAwakeLate()
        { }

        // Play
#pragma warning disable IDE0051 // Remove unused private members
        private void Awake()
        {
            PlayAwake();
            PlayAwakeLate();
        }
        private void OnEnable()
        => PlayEnable();
        private void Start()
        => PlayStart();
        private void OnDisable()
        => PlayDisable();
        private void OnDestroy()
        => PlayDestroy();
    }
}
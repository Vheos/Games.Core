namespace Vheos.Tools.UnityCore
{
    using UnityEngine;
    /// <summary> Wraps and virtualizes common MonoBehaviour events. </summary>
    abstract public class APlayable : MonoBehaviour
    {
        // Protected
        /// <summary> Wraps Unity's <see langword="Awake"/>(). </summary>
        virtual protected void PlayAwake()
        { }
        /// <summary> Wraps Unity's <see langword="OnEnable"/>(). </summary>
        virtual protected void PlayEnable()
        { }
        /// <summary> Wraps Unity's <see langword="Start"/>(). </summary>
        virtual protected void PlayStart()
        { }
        /// <summary> Wraps Unity's <see langword="OnDisable"/>(). </summary>
        virtual protected void PlayDisable()
        { }
        /// <summary> Wraps Unity's <see langword="OnDestroy"/>(). </summary>
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
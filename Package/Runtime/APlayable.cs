namespace Vheos.Tools.UnityCore
{
    using UnityEngine;
    abstract public class APlayable : MonoBehaviour
    {
        // Virtuals
        virtual protected void PlayAwake()
        { }
        virtual protected void PlayEnable()
        { }
        virtual protected void PlayStart()
        { }
        virtual protected void PlayDisable()
        { }
        virtual protected void PlayDestroy()
        { }

        // Play
        private void Awake()
        => PlayAwake();
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
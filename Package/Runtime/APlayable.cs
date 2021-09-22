namespace Vheos.Tools.UnityCore
{
    using UnityEngine;
    abstract public class APlayable : MonoBehaviour
    {
        // Virtuals
        virtual public void PlayAwake()
        { }
        virtual public void PlayStart()
        { }
        virtual public void PlayEnable()
        { }
        virtual public void PlayDisable()
        { }
        virtual public void PlayDestroy()
        { }

        // Mono
        private void Awake()
        => PlayAwake();
        private void Start()
        => PlayStart();
        private void OnEnable()
        => PlayEnable();
        private void OnDisable()
        => PlayDisable();
        private void OnDestroy()
        => PlayDestroy();
    }
}
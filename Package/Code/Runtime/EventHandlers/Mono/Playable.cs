namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Playable : ABaseComponent
    {
        // Events
        public AutoEvent OnAwake
        { get; } = new AutoEvent();
        public AutoEvent OnEnable
        { get; } = new AutoEvent();
        public AutoEvent OnStart
        { get; } = new AutoEvent();
        public AutoEvent OnDisable
        { get; } = new AutoEvent();
        public AutoEvent OnDestroy
        { get; } = new AutoEvent();

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            OnAwake.Invoke();
        }
        protected override void PlayEnable()
        {
            base.PlayEnable();
            OnEnable.Invoke();
        }
        protected override void PlayStart()
        {
            base.PlayStart();
            OnStart.Invoke();
        }
        protected override void PlayDisable()
        {
            base.PlayDisable();
            OnDisable.Invoke();
        }
        protected override void PlayDestroy()
        {
            base.PlayDestroy();
            OnDestroy.Invoke();
        }
    }
}
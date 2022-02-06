namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    abstract public class AStaticComponent<T> : ABaseComponent where T : AStaticComponent<T>
    {
        // Privates
        static protected T _instance;

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _instance = this as T;
        }
    }
}
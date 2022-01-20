namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    abstract public class AGlobalComponent<T> : ABaseComponent where T : AGlobalComponent<T>
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
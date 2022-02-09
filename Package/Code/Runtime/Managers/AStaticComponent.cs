namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    abstract public class AStaticComponent<T> : ABaseComponent where T : AStaticComponent<T>
    {
        // Privates
        static public T Instance { get; private set; }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            Instance = this as T;
        }
    }
}
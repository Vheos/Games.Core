namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    abstract public class AManager<T> : AAutoSubscriber where T : AManager<T>
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
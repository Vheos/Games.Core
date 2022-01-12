namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    public enum SceneOperation
    {
        Load,
        Unload,
    }
    public enum SceneOperationTiming
    {
        Synchronously,
        Asynchronously,
    }
}
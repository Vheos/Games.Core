namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    // Defines
    [Serializable]
    public class SceneOperationOrder
    {
        public SceneOperation FirstOperation = SceneOperation.Unload;
        public SceneOperationTiming FirstOperationTiming = SceneOperationTiming.Synchronously;
        public SceneOperation SecondOperation = SceneOperation.Load;
        public SceneOperationTiming SecondOperationTiming = SceneOperationTiming.Synchronously;
    }
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
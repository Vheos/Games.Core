namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct SceneOperationOrder
    {
        public SceneOperation FirstOperation;
        public SceneOperationTiming FirstOperationTiming;
        public SceneOperation SecondOperation;
        public SceneOperationTiming SecondOperationTiming;
    }
}
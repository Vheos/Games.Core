namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    public struct EventInfo
    {
        public readonly EventThresholdType ThresholdType;
        public readonly float Threshold;
        public readonly Action Action;

        // Initializers
        public EventInfo(EventThresholdType thresholdType, float threshold, Action action)
        {
            ThresholdType = thresholdType;
            Threshold = threshold;
            Action = action;
        }
    }
}
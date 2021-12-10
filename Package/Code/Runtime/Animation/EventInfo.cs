namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    public struct EventInfo
    {
        // Publics
        public EventThresholdType ThresholdType;
        public float Threshold;
        public Action Action;

        // Initializers
        public EventInfo(float threshold, Action action)
        {
            ThresholdType = EventThresholdType.Progress;
            Threshold = threshold;
            Action = action;
        }
        public EventInfo(EventThresholdType thresholdType, float threshold, Action action)
        {
            ThresholdType = thresholdType;
            Threshold = threshold;
            Action = action;
        }
    }
}
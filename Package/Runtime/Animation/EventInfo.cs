namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public struct EventInfo
    {
        // Publics
        public EventThresholdType ThresholdType;
        public float Threshold;
        public Action Action;
        public bool IsOnHasFinished
        => ThresholdType == EventThresholdType.Progress && Threshold >= 1f;

        // Initializers
        public EventInfo(Action action)
        {
            ThresholdType = EventThresholdType.Progress;
            Threshold = 1f;
            Action = action;
        }
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

        // Operators
        static public implicit operator EventInfo[](EventInfo t)
        => new[] { t };
    }
}
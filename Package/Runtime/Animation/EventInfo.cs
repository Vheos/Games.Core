namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    public class EventInfo
    {
        public readonly EventThresholdType ThresholdType;
        public readonly float Threshold;
        public readonly Action Action;
        public readonly bool IsOnHasFinished;

        // Initializers
        public EventInfo(Action action)
        {
            IsOnHasFinished = true;
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
    }
}
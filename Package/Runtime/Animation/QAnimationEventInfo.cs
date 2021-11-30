namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    public struct QAnimationEventInfo
    {
        public readonly AnimationEventThresholdType ThresholdType;
        public readonly float Threshold;
        public readonly Action Action;

        // Initializers
        public QAnimationEventInfo(AnimationEventThresholdType thresholdType, float threshold, Action action)
        {
            ThresholdType = thresholdType;
            Threshold = threshold;
            Action = action;
        }
    }

    public enum AnimationEventThresholdType
    {
        Time = 0,
        Progress,
        Value,
    }
}
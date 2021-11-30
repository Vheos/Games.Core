namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    public enum EventThresholdType
    {
        Time = 0,
        Progress,
        Value,
    }

    public enum TimeDeltaType
    {
        Scaled = 0,
        Unscaled,
    }

    public enum AssignmentType
    {
        Additive = 0,
        Multiplicative,
    }
}
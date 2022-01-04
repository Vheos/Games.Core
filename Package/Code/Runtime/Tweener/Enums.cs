namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    public enum CurveFuncType
    {
        Normal = 0,
        Inverted,
        Mirror,
        MirrorInverted,
        Bounce,
        BounceInverted,
    }

    public enum ConflictResolution
    {
        Blend = 0,
        Wait,
        Interrupt,        
        DoNothing,
    }

    /// <summary> Maps to a specific <c><see cref="Tween"/></c>'s property </summary>
    /// <remarks> Used in <c><see cref="EventInfo.ThresholdVariable"/></c> </remarks>
    public enum EventThresholdVariable
    {
        /// <summary> The <c><see cref="Tween"/></c>'s progress in time, ranging from <c>0f</c> to <c>1f</c> <br/> </summary>
        /// <remarks> Value of <c>0.5f</c> means "halfway through the tween's duration" </remarks>
        Progress,
        /// <summary> The <c><see cref="Tween"/></c>'s total elapsed time, ranging from <c>0f</c> to the tween's total duration </summary>
        /// <remarks> Value of <c>0.5f</c> means "half a second after the tween's start" </remarks>
        ElapsedTime,
        /// <summary> The <c><see cref="Tween"/></c>'s total elapsed time, ranging from <c>0f</c> to the tween's total duration </summary>
        /// <remarks> Value of <c>0.5f</c> means "when the tween's curve value increases or decreases to <c>0.5f</c>" </remarks>
        CurveValue,
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
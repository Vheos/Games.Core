namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    /// <summary> Modifies the shape of <c><see cref="AnimationCurve"/></c> </summary>
    /// <remarks> Used in <c><see cref="Tween.SetCurveShape(CurveShape)"/></c></remarks>
    public enum CurveShape
    {
        /// <summary> The curve isn't changed </summary>
        Normal,
        /// <summary> The curve is flipped along the XY axis </summary>
        Invert,
        /// <summary> The curve runs from start to end and then backwards (twice as fast) </summary>
        Mirror,
        /// <summary> First <c>Invert</c>s the curve, then <c>Mirror</c>s it </summary>
        InvertAndMirror,
        /// <summary> The curve values are doubled, and they "bounce" down if over <c>1f</c> </summary>
        Bounce,
        /// <summary> First <c>Invert</c>s the curve, then <c>Bounce</c>s it </summary>
        InvertAndBounce,
    }

    /// <summary> Controls how a conflict between 2 (or more) <c><see cref="Tween"/></c>s on the same layer is resolved </summary>
    /// <remarks> Used in <c><see cref="Tween.SetConflictResolution(ConflictResolution)"/></c> </remarks>
    public enum ConflictResolution
    {
        /// <summary> The tweens ignore each other and play independently </summary>
        Blend,
        /// <summary> The new tween waits until the ongoing tweens finish </summary>
        Wait,
        /// <summary> The onging tweens stop instantly, then new tween starts </summary>
        Interrupt,
        /// <summary> The new tween doesn't start </summary>
        DoNothing,
    }

    /// <summary> Maps to a <c><see cref="Tween"/></c>'s specific property </summary>
    /// <remarks> Used in <c><see cref="EventInfo.EventInfo(EventThresholdVariable, float, Action)"/></c> </remarks>
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

    /// <summary> Controls how delta time is calculated </summary>
    /// <remarks> Used in <c><see cref="Tween.SetDeltaTime(DeltaTimeType)"/></c> </remarks>
    public enum DeltaTimeType
    {
        /// <summary> The <c><see cref="Tween"/></c>'s delta time is scaled by <c><see cref="Time.timeScale"/></c> </summary> 
        Scaled,
        /// <summary> The <c><see cref="Tween"/></c>'s delta time mirrors real time </summary> 
        Realtime,
    }

    /// <summary> Controls how delta value is calculated </summary>
    /// <remarks> Used in <c><see cref="Tween.AddPropertyModifier{T}(Action{T}, T, DeltaValueType)"/></c> </remarks>
    public enum DeltaValueType
    {
        /// <summary> The <c><see cref="Tween"/></c>'s delta value is equal to: <c><c><see langword="CurrentFrameValue"/></c> - <c><see langword="PreviousFrameValue"/></c></c> </summary>
        /// <remarks> Use with additive functions, such as: <c>UserProperty += deltaValue</c></remarks>    
        Offset,
        /// <summary> The <c><see cref="Tween"/></c>'s delta value is equal to: <c><c><see langword="CurrentFrameValue"/></c> / <c><see langword="PreviousFrameValue"/></c></c> </summary>
        /// <remarks> Use with multiplicative functions, such as: <c>UserProperty *= deltaValue</c></remarks>    
        Ratio,
    }
}
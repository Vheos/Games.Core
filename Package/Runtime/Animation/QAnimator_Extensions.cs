namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.Math;

    static public class QAnimator_Extensions
    {
        // Basic
        static public void AnimatePosition(this Component t, Vector3 value, float duration)
        => QAnimator.Animate(t.AssignPositionFunc, value, Qurve.ValuesByProgress, duration);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, Qurve.ValuesByProgress, duration);
        static public void AnimateRotation(this Component t, Quaternion value, float duration)
        => QAnimator.Animate(t.AssignRotationFunc, value, Qurve.ValuesByProgress, duration);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, Qurve.ValuesByProgress, duration);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, Qurve.ValuesByProgress, duration, default, default, AssignmentType.Multiplicative);

        // ConflictResolution
        static public void AnimatePosition(this Component t, Vector3 value, float duration, ConflictResolution conflictResolution, object guid)
        => QAnimator.Animate(t.AssignPositionFunc, value, Qurve.ValuesByProgress, duration, default, default, default, conflictResolution, guid);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, ConflictResolution conflictResolution, object guid)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, Qurve.ValuesByProgress, duration, default, default, default, conflictResolution, guid);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, ConflictResolution conflictResolution, object guid)
        => QAnimator.Animate(t.AssignRotationFunc, value, Qurve.ValuesByProgress, duration, default, default, default, conflictResolution, guid);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, ConflictResolution conflictResolution, object guid)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, Qurve.ValuesByProgress, duration, default, default, default, conflictResolution, guid);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, ConflictResolution conflictResolution, object guid)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, Qurve.ValuesByProgress, duration, default, default, AssignmentType.Multiplicative, conflictResolution, guid);

        // Curve
        static public void AnimatePosition(this Component t, Vector3 value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignPositionFunc, value, curve, duration);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, curve, duration);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignRotationFunc, value, curve, duration);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, curve, duration);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, curve, duration, default, default, AssignmentType.Multiplicative);
        // Events
        static public void AnimatePosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignPositionFunc, value, Qurve.ValuesByProgress, duration, eventInfos);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, Qurve.ValuesByProgress, duration, eventInfos);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignRotationFunc, value, Qurve.ValuesByProgress, duration, eventInfos);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, Qurve.ValuesByProgress, duration, eventInfos);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, Qurve.ValuesByProgress, duration, eventInfos, default, AssignmentType.Multiplicative);

        // Privates
        static private void AssignPositionFunc(this Component t, Vector3 v)
        => t.transform.position += v;
        static private void AssignLocalPositionFunc(this Component t, Vector3 v)
        => t.transform.localPosition += v;
        static private void AssignRotationFunc(this Component t, Quaternion v)
        => t.transform.rotation = t.transform.rotation.Add(v);
        static private void AssignLocalRotationFunc(this Component t, Quaternion v)
        => t.transform.localRotation = t.transform.localRotation.Add(v);
        static private void AssignLocalScaleFunc(this Component t, Vector3 v)
        => t.transform.localScale = t.transform.localScale.Mul(v);
    }
}
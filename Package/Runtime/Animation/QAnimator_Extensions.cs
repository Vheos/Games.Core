namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.Math;

    static public class QAnimator_Extensions
    {

        static public void AnimatePosition(this Component t, Vector3 value, float duration)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, curve);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, eventInfos);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, guid, conflictResolution);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, curve, eventInfos);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, curve, guid, conflictResolution);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, eventInfos, guid, conflictResolution);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, value, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, curve);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, eventInfos);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, guid, conflictResolution);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, curve, eventInfos);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, curve, guid, conflictResolution);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, eventInfos, guid, conflictResolution);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, value, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion value, float duration)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, curve);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, eventInfos);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, guid, conflictResolution);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, curve, eventInfos);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, curve, guid, conflictResolution);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, eventInfos, guid, conflictResolution);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, value, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, curve);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, eventInfos);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, guid, conflictResolution);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, curve, eventInfos);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, curve, guid, conflictResolution);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, eventInfos, guid, conflictResolution);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, value, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, curve, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, eventInfos, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, curve, eventInfos, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, curve, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, curve, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, eventInfos, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, curve, eventInfos, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, curve, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, value, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);

        // Privates
        static private void AssignPositionFunc(this Component t, Vector3 a)
        => t.transform.position += a;
        static private void AssignLocalPositionFunc(this Component t, Vector3 a)
        => t.transform.localPosition += a;
        static private void AssignRotationFunc(this Component t, Quaternion a)
        => t.transform.rotation = t.transform.rotation.Add(a);
        static private void AssignLocalRotationFunc(this Component t, Quaternion a)
        => t.transform.localRotation = t.transform.localRotation.Add(a);
        static private void AssignLocalScaleFunc(this Component t, Vector3 a)
        => t.transform.localScale = t.transform.localScale.Mul(a);
    }
}
namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.Math;

    static public class QAnimator_Extensions
    {
        // Transform.position
        static public void AnimatePosition(this Component t, Vector3 to, float duration)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, curve);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, eventInfos);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, guid, conflictResolution);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, curve, eventInfos);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, curve, guid, conflictResolution);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, eventInfos, guid, conflictResolution);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        // Transform.localPosition
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, curve);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, eventInfos);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, guid, conflictResolution);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, curve, eventInfos);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, curve, guid, conflictResolution);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, eventInfos, guid, conflictResolution);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        // Transform.rotation
        static public void AnimateRotation(this Component t, Quaternion to, float duration)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, curve);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, eventInfos);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, guid, conflictResolution);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, curve, eventInfos);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, curve, guid, conflictResolution);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, eventInfos, guid, conflictResolution);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        // Transform.localRotation
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, curve);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, eventInfos);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, guid, conflictResolution);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, curve, eventInfos);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, curve, guid, conflictResolution);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, eventInfos, guid, conflictResolution);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        // Transform.localScale
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, curve);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, eventInfos);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, guid, conflictResolution);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, curve, eventInfos);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, curve, guid, conflictResolution);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, eventInfos, guid, conflictResolution);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        // Transform.localScale (ratio)
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, curve, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, eventInfos, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, curve, eventInfos, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, curve, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, curve, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, eventInfos, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, curve, eventInfos, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, curve, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignLocalScaleRatioFunc, ratio, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        // SpriteRenderer.color
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, curve);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, eventInfos);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, guid, conflictResolution);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, AssignmentType.Additive, timeDeltaType);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, curve, eventInfos);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, curve, guid, conflictResolution);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, curve, AssignmentType.Additive, timeDeltaType);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, eventInfos, guid, conflictResolution);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, curve, eventInfos, AssignmentType.Additive, timeDeltaType);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, curve, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Additive, timeDeltaType);
        // SpriteRenderer.color (ratio)
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, AssignmentType.Multiplicative, default);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, AnimationCurve curve)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, curve, AssignmentType.Multiplicative, default);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, eventInfos, AssignmentType.Multiplicative, default);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, curve, eventInfos, AssignmentType.Multiplicative, default);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, curve, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, curve, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, eventInfos, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, default);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, curve, eventInfos, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, curve, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, TimeDeltaType timeDeltaType)
        => QAnimator.Animate(t.AssignColorRatioFunc, ratio, duration, curve, eventInfos, guid, conflictResolution, AssignmentType.Multiplicative, timeDeltaType);

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
        => t.transform.localScale += a;
        static private void AssignLocalScaleRatioFunc(this Component t, Vector3 a)
        => t.transform.localScale = t.transform.localScale.Mul(a);
        static private void AssignColorFunc(this SpriteRenderer t, Color a)
        => t.color += a;
        static private void AssignColorRatioFunc(this SpriteRenderer t, Color a)
        => t.color *= a;
        static private Vector3 PositionOffsetTo(this Component t, Vector3 a)
        => a - t.transform.position;
        static private Vector3 LocalPositionOffsetTo(this Component t, Vector3 a)
        => a - t.transform.localPosition;
        static private Quaternion RotationOffsetTo(this Component t, Quaternion a)
        => a.Sub(t.transform.rotation);
        static private Quaternion LocalRotationOffsetTo(this Component t, Quaternion a)
        => a.Sub(t.transform.localRotation);
        static private Vector3 LocalScaleOffsetTo(this Component t, Vector3 a)
        => a - t.transform.localScale;
        static private Color ColorOffsetTo(this SpriteRenderer t, Color a)
        => a - t.color;
    }
}
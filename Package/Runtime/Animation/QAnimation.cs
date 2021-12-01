namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Math;

    internal class QAnimation<T> : AQAnimation where T : struct
    {
        // Privates
        private Action GetAssignInvoke<TNested>(Action<TNested> assignFunc, TNested value, AssignmentType assignType) where TNested : struct
        => assignType switch
        {
            AssignmentType.Additive => new GenericArgs<TNested>(assignFunc, value) switch
            {
                GenericArgs<float> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Vector2> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Vector3> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Vector4> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Color> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Quaternion> t => () => t.AssignFunc(Quaternion.identity.Lerp(t.Value, CurveValueDelta)),
                _ => throw AnimationNotSupportedException<TNested>(assignType),
            },
            AssignmentType.Multiplicative => new GenericArgs<TNested>(assignFunc, value) switch
            {
                GenericArgs<float> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                GenericArgs<Vector2> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                GenericArgs<Vector3> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                GenericArgs<Vector4> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                GenericArgs<Color> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                _ => throw AnimationNotSupportedException<TNested>(assignType),
            },
            _ => throw AnimationNotSupportedException<TNested>(assignType),
        };
        private NotSupportedException AnimationNotSupportedException<TNested>(AssignmentType assignType) where TNested : struct
        => new NotSupportedException($"{assignType} {typeof(TNested).Name} animation is not supported!");

        // Initializers
        internal QAnimation(Action<T> assignFunc, T value, AnimationCurve curve, float duration,
            IEnumerable<EventInfo> eventInfos = null, TimeDeltaType deltaType = default, AssignmentType assignType = default, object guid = default)
            : base(curve, duration, eventInfos, deltaType, guid)
        => _assignInvoke = GetAssignInvoke(assignFunc, value, assignType);

        // Defines
        private class GenericArgs<TNested> where TNested : struct
        {
            // Publics
            public Action<TNested> AssignFunc;
            public TNested Value;

            // Initializers
            public GenericArgs(Action<TNested> assignFunc, TNested value)
            {
                AssignFunc = assignFunc;
                Value = value;
            }
        }
    }
}
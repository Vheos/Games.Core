namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Math;
    using Vheos.Tools.Extensions.Collections;

    internal class QAnimation
    {
        // Internals
        internal event Action OnHasFinished;
        internal void InvokeOnHasFinished()
        => OnHasFinished?.Invoke();
        internal bool IsInstant
        => _duration <= 0;
        internal bool HasAnyAssignments
        => _assignInvoke != null;
        internal bool HasFinished
        => _elapsed.Current >= _duration;
        internal void Process()
        {
            _elapsed.Previous = _elapsed.Current;
            _elapsed.Current += _timeDeltaFunc();
            if (_assignInvoke == null)
                return;

            _progress.Previous = _progress.Current;
            _progress.Current = _elapsed.Current.Div(_duration).ClampMax(1f);
            _curveValue.Previous = _curveValue.Current;
            _curveValue.Current = _curveValueFunc(_progress.Current);
            _assignInvoke();
            if (_events == null)
                return;

            foreach (var @event in _events)
                @event.TryInvoke();
        }
        internal object GUID
        { get; }
        internal void AddAssignment<T>(Action<T> assignFunc, T value, AssignmentType assignType) where T : struct
        => _assignInvoke += GetAssignInvoke(assignFunc, value, assignType);

        // Privates
        private readonly float _duration;
        private readonly Func<float> _timeDeltaFunc;
        private readonly Func<float, float> _curveValueFunc;
        private HashSet<Event> _events;
        private Action _assignInvoke;
        private (float Current, float Previous) _elapsed, _progress, _curveValue;
        private Func<(float, float)> GetEventValuePairFunc(EventThresholdType thresholdType)
        => thresholdType switch
        {
            EventThresholdType.Time => () => _elapsed,
            EventThresholdType.Progress => () => _progress,
            EventThresholdType.Value => () => _curveValue,
            _ => () => default,
        };
        private Func<float> GetTimeDeltaFunc(TimeDeltaType timeDeltaType)
        => timeDeltaType switch
        {
            TimeDeltaType.Scaled => () => Time.deltaTime,
            TimeDeltaType.Unscaled => () => Time.unscaledDeltaTime,
            _ => () => default,
        };
        private void InitializeEvents(IEnumerable<EventInfo> eventInfos)
        {
            var newEvents = new HashSet<Event>();
            foreach (var eventInfo in eventInfos)
                if (eventInfo.IsOnHasFinished)
                    OnHasFinished += eventInfo.Action;
                else
                    newEvents.Add(new Event(eventInfo.Threshold, eventInfo.Action, GetEventValuePairFunc(eventInfo.ThresholdType)));

            if (newEvents.IsNotEmpty())
                _events = newEvents;
        }
        private float CurveValueDelta
            => _curveValue.Current - _curveValue.Previous;
        private Action GetAssignInvoke<T>(Action<T> assignFunc, T value, AssignmentType assignType) where T : struct
        => assignType switch
        {
            AssignmentType.Additive => new GenericArgs<T>(assignFunc, value) switch
            {
                GenericArgs<float> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Vector2> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Vector3> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Vector4> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Color> t => () => t.AssignFunc(t.Value * CurveValueDelta),
                GenericArgs<Quaternion> t => () => t.AssignFunc(Quaternion.identity.SLerp(t.Value, CurveValueDelta)),
                _ => throw AnimationNotSupportedException<T>(assignType),
            },
            AssignmentType.Multiplicative => new GenericArgs<T>(assignFunc, value) switch
            {
                GenericArgs<float> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                GenericArgs<Vector2> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                GenericArgs<Vector3> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                GenericArgs<Vector4> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                GenericArgs<Color> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta)),
                _ => throw AnimationNotSupportedException<T>(assignType),
            },
            _ => throw AnimationNotSupportedException<T>(assignType),
        };
        private NotSupportedException AnimationNotSupportedException<T>(AssignmentType assignType) where T : struct
        => new NotSupportedException($"{assignType} {typeof(T).Name} animation is not supported!");
        private Func<float, float> GetCurveValueFunc(AnimationCurve curve, CurveFuncType funcType)
        => funcType switch
        {
            CurveFuncType.Normal => p => curve.Evaluate(p),
            CurveFuncType.Inverted => p => 1f - curve.Evaluate(1f - p),
            CurveFuncType.Mirror => p => curve.Evaluate(2 * (p <= 0.5f ? p : 1f - p)),
            CurveFuncType.MirrorInverted => p => 1f - curve.Evaluate(1f - 2 * (p <= 0.5f ? p : 1f - p)),
            CurveFuncType.Bounce => p => 1f - (2 * curve.Evaluate(p) - 1f).Abs(),
            CurveFuncType.BounceInverted => p => 1f - (2 * curve.Evaluate(1f - p) - 1f).Abs(),
            _ => t => 0f,
        };

        // Initializers
        internal QAnimation(float duration)
        {
            _duration = duration;
            _curveValueFunc = GetCurveValueFunc(Qurve.ValuesByProgress, CurveFuncType.Normal);
            _timeDeltaFunc = GetTimeDeltaFunc(TimeDeltaType.Scaled);
            GUID = null;
        }
        internal QAnimation(float duration, OptionalParameters optionals)
        {
            _duration = duration;
            _curveValueFunc = GetCurveValueFunc(optionals.Curve ?? Qurve.ValuesByProgress, optionals.CurveFuncType ?? CurveFuncType.Normal);
            _timeDeltaFunc = GetTimeDeltaFunc(optionals.TimeDeltaType ?? TimeDeltaType.Scaled);
            GUID = optionals.GUID;
            if (optionals.EventInfo != null)
                InitializeEvents(optionals.EventInfo);
        }

        // Defines
        private class GenericArgs<T> where T : struct
        {
            // Publics
            public Action<T> AssignFunc;
            public T Value;

            // Initializers
            public GenericArgs(Action<T> assignFunc, T value)
            {
                AssignFunc = assignFunc;
                Value = value;
            }
        }

        // Defines
        private class Event
        {
            // Privates
            internal void TryInvoke()
            {
                if (Test())
                    _action.Invoke();
            }
            private readonly float _threshold;
            private readonly Action _action;
            private readonly Func<(float, float)> _valuePairFunc;
            private bool Test()
            {
                (float Current, float Previous) = _valuePairFunc();
                return Previous < _threshold && Current >= _threshold
                    || Previous > _threshold && Current <= _threshold;
            }

            // Initializers
            internal Event(float threshold, Action action, Func<(float, float)> valuePairFunc)
            {
                _threshold = threshold;
                _action = action;
                _valuePairFunc = valuePairFunc;
            }
        }
    }
}
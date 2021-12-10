namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Math;
    using Vheos.Tools.Extensions.Collections;

    public class QAnimation
    {
        // Publics
        public QAnimation Custom<T>(Action<T> assignFunc, T value) where T : struct
        {
            _assignInvoke += GetAssignInvoke(assignFunc, value, AssignmentType.Additive);
            return this;
        }
        public QAnimation Custom<T>(Action<T> assignFunc, T value, AssignmentType assignType) where T : struct
        {
            _assignInvoke += GetAssignInvoke(assignFunc, value, assignType);
            return this;
        }
        public QAnimation Events(params EventInfo[] eventInfos)
        {
            Func<(float, float)> GetEventValuePairFunc(EventThresholdType thresholdType)
            => thresholdType switch
            {
                EventThresholdType.Time => () => _elapsed,
                EventThresholdType.Progress => () => _progress,
                EventThresholdType.Value => () => _curveValue,
                _ => () => default,
            };

            foreach (var eventInfo in eventInfos)
            {
                _events ??= new HashSet<Event>();
                _events.Add(new Event(eventInfo.Threshold, eventInfo.Action, GetEventValuePairFunc(eventInfo.ThresholdType)));
            }

            return this;
        }
        public QAnimation Events(params Action[] onFinishEvents)
        {
            foreach (var @event in onFinishEvents)
                OnFinish += @event;

            return this;
        }
        public QAnimation Set(AnimationCurve curve)
        {
            _curve = curve;
            return this;
        }
        public QAnimation Set(CurveFuncType curveFuncType)
        {
            _curveValueFunc = GetCurveValueFunc(curveFuncType);
            return this;
        }
        public QAnimation Set(TimeDeltaType timeDeltaType)
        {
            _timeDeltaFunc = GetTimeDeltaFunc(timeDeltaType);
            return this;
        }
        public QAnimation Set(object guid)
        {
            GUID = guid;
            return this;
        }
        public QAnimation Set(ConflictResolution conflictResolution)
        {
            ConflictResolution = conflictResolution;
            return this;
        }

        // Internals        
        internal void InvokeOnFinish()
        => OnFinish?.Invoke();
        internal bool HasFinished
        => _elapsed.Current >= _duration;
        internal bool HasGUID()
        => GUID != null;
        internal bool HasGUID(object guid)
        => GUID == guid;
        internal void InitializeLate()
        {
            _curve ??= Qurve.ValuesByProgress;
            _curveValueFunc ??= GetCurveValueFunc(CurveFuncType.Normal);
            _timeDeltaFunc ??= GetTimeDeltaFunc(TimeDeltaType.Scaled);
        }
        internal void InstantFinish()
        {
            _curveValue.Current = _curveValueFunc(1f);
            _assignInvoke?.Invoke();
            OnFinish?.Invoke();
        }
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

        // Settings
        private readonly float _duration;
        private Action _assignInvoke;
        private HashSet<Event> _events;
        private Action OnFinish;
        private AnimationCurve _curve;
        private Func<float, float> _curveValueFunc;
        private Func<float> _timeDeltaFunc;
        internal object GUID
        { get; private set; }
        internal ConflictResolution ConflictResolution
        { get; private set; }

        // Privates (helpers)
        private (float Current, float Previous) _elapsed, _progress, _curveValue;
        private Action GetAssignInvoke<T>(Action<T> assignFunc, T value, AssignmentType assignType) where T : struct
        {
            float CurveValueDelta()
            => _curveValue.Current - _curveValue.Previous;

            return assignType switch
            {
                AssignmentType.Additive => new GenericArgs<T>(assignFunc, value) switch
                {
                    GenericArgs<float> t => () => t.AssignFunc(t.Value * CurveValueDelta()),
                    GenericArgs<Vector2> t => () => t.AssignFunc(t.Value * CurveValueDelta()),
                    GenericArgs<Vector3> t => () => t.AssignFunc(t.Value * CurveValueDelta()),
                    GenericArgs<Vector4> t => () => t.AssignFunc(t.Value * CurveValueDelta()),
                    GenericArgs<Color> t => () => t.AssignFunc(t.Value * CurveValueDelta()),
                    GenericArgs<Quaternion> t => () => t.AssignFunc(Quaternion.identity.SLerp(t.Value, CurveValueDelta())),
                    _ => throw AnimationNotSupportedException<T>(assignType),
                },
                AssignmentType.Multiplicative => new GenericArgs<T>(assignFunc, value) switch
                {
                    GenericArgs<float> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta())),
                    GenericArgs<Vector2> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta())),
                    GenericArgs<Vector3> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta())),
                    GenericArgs<Vector4> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta())),
                    GenericArgs<Color> t => () => t.AssignFunc(t.Value.Pow(CurveValueDelta())),
                    _ => throw AnimationNotSupportedException<T>(assignType),
                },
                _ => throw AnimationNotSupportedException<T>(assignType),
            };
        }
        private Func<float, float> GetCurveValueFunc(CurveFuncType funcType)
        => funcType switch
        {
            CurveFuncType.Normal => p => _curve.Evaluate(p),
            CurveFuncType.Inverted => p => 1f - _curve.Evaluate(1f - p),
            CurveFuncType.Mirror => p => _curve.Evaluate(2 * (p <= 0.5f ? p : 1f - p)),
            CurveFuncType.MirrorInverted => p => 1f - _curve.Evaluate(1f - 2 * (p <= 0.5f ? p : 1f - p)),
            CurveFuncType.Bounce => p => 1f - (2 * _curve.Evaluate(p) - 1f).Abs(),
            CurveFuncType.BounceInverted => p => 1f - (2 * _curve.Evaluate(1f - p) - 1f).Abs(),
            _ => t => 0f,
        };
        private Func<float> GetTimeDeltaFunc(TimeDeltaType timeDeltaType)
        => timeDeltaType switch
        {
            TimeDeltaType.Scaled => () => Time.deltaTime,
            TimeDeltaType.Unscaled => () => Time.unscaledDeltaTime,
            _ => () => default,
        };
        private NotSupportedException AnimationNotSupportedException<T>(AssignmentType assignType) where T : struct
        => new NotSupportedException($"{assignType} {typeof(T).Name} animation is not supported!");

        // Initializers
        private QAnimation()
        { }
        internal QAnimation(float duration)
        {
            _duration = duration;
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
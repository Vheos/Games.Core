namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Math;

    public class Tween
    {
        // Publics
        /// <summary> Adds a modifier function for <c><see langword="YourProperty"/></c></summary>
        /// <param name="function">
        ///     This is the function that will modify <c><see langword="YourProperty"/></c> on each tween update.<br/>
        ///     Its input is the partial delta (from last tween update) and is affected by <c><paramref name="type"/></c>:<br/>
        ///     • <c><see cref="AssignmentType.Additive"/></c> - the partial delta is an offset, so additive assignment (<c>+=</c>) should be used<br/>
        ///     • <c><see cref="AssignmentType.Multiplicative"/></c> - the partial delta is a ratio, so multiplicative assignment (<c>+=</c>) should be used<br/>
        ///     <br/>
        ///     Recommended lamba expression format: <c>dV => <c><see langword="YourProperty"/></c> ?= dV</c><br/>
        /// </param>
        /// <param name="totalDelta">
        ///     This is the total change that you want applied to <c><see langword="YourProperty"/></c> over the course of the tween:<br/>
        ///     • <c><see cref="AssignmentType.Additive"/></c> - the <c><paramref name="totalDelta"/></c> is an offset, so it will be added to <c><see langword="YourProperty"/></c><br/>
        ///     • <c><see cref="AssignmentType.Multiplicative"/></c> - the <c><paramref name="totalDelta"/></c> is a ratio, so <c><see langword="YourProperty"/></c> will be multiplied by it<br/>
        /// </param>
        /// <param name="type">
        ///     This parameter controls how the other parameters (<c><paramref name="function"/></c> and <c><paramref name="totalDelta"/></c>) are interpreted and used<br/>
        ///     Check their respective documentations for more info on how <c><paramref name="type"/></c> affects them
        /// </param>
        public Tween AddModifier<T>(Action<T> function, T totalDelta, AssignmentType type = AssignmentType.Additive) where T : struct
        {
            _assignInvoke += GetAssignInvoke(function, totalDelta, type);
            return this;
        }
        /// <summary> Adds timed events to this tween</summary>
        /// <param name="eventInfos">
        ///     This is the collection of <c><see cref="EventInfo"/></c>s that will be converted to internal events
        /// </param>
        public Tween AddEvents(params EventInfo[] eventInfos)
        {
            Func<(float, float)> GetEventValuePairFunc(EventThresholdVariable thresholdType)
            => thresholdType switch
            {
                EventThresholdVariable.Progress => () => _progress,
                EventThresholdVariable.ElapsedTime => () => _elapsed,
                EventThresholdVariable.CurveValue => () => _curveValue,
                _ => () => default,
            };

            foreach (var eventInfo in eventInfos)
            {
                _events ??= new HashSet<Event>();
                _events.Add(new Event(eventInfo.Threshold, eventInfo.Action, GetEventValuePairFunc(eventInfo.ThresholdVariable)));
            }

            return this;
        }
        public Tween AddOnFinishEvents(params Action[] onFinishEvents)
        {
            foreach (var @event in onFinishEvents)
                OnFinish += @event;

            return this;
        }
        public Tween SetCurve(AnimationCurve curve)
        {
            _curve = curve;
            return this;
        }
        public Tween SetFunctionType(CurveFuncType curveFuncType)
        {
            _curveValueFunc = GetCurveValueFunc(curveFuncType);
            return this;
        }
        public Tween SetTimeDelta(TimeDeltaType timeDeltaType)
        {
            _timeDeltaFunc = GetTimeDeltaFunc(timeDeltaType);
            return this;
        }
        public Tween SetGUID(object guid)
        {
            GUID = guid;
            return this;
        }
        public Tween SetConflictResolution(ConflictResolution conflictResolution)
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
        internal void FinishInstantly()
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
        private Tween()
        {

        }
        internal Tween(float duration) : this()
        => _duration = duration;

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
namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Math;

    abstract internal class AQAnimation
    {
        // Defaults
        static internal GUID DefaultGUID
        { get; private set; }
        static internal Func<float> DefaultTimeDeltaFunc
        { get; private set; }

        // Constructor args
        private readonly float _duration;
        private readonly AnimationCurve _curve;
        private readonly Func<float> _timeDeltaFunc;
        private readonly HashSet<Event> _events;
        private (float Current, float Previous) _curveTime, _curveProgress, _curveValue;
        private Func<(float, float)> GetEventValuePairFunc(EventThresholdType thresholdType)
        => thresholdType switch
        {
            EventThresholdType.Time => () => _curveTime,
            EventThresholdType.Progress => () => _curveProgress,
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
            foreach (var eventInfo in eventInfos)
                _events.Add(new Event(eventInfo.Threshold, eventInfo.Action, GetEventValuePairFunc(eventInfo.ThresholdType)));
        }

        // for QAnimation<T>
        protected float CurveValueDelta
            => _curveValue.Current - _curveValue.Previous;
        protected Action _assignInvoke;

        // for QAnimator
        internal event Action OnHasFinished;
        internal void InvokeOnHasFinished()
        => OnHasFinished?.Invoke();
        internal bool HasFinished
        => _curveTime.Current >= _duration;
        internal void Process()
        {
            _curveTime.Previous = _curveTime.Current;
            _curveTime.Current += _timeDeltaFunc();
            _curveProgress.Previous = _curveProgress.Current;
            _curveProgress.Current = _curveTime.Current.Div(_duration).ClampMax(1f);
            _curveValue.Previous = _curveValue.Current;
            _curveValue.Current = _curve.Evaluate(_curveProgress.Current);

            _assignInvoke();
            if (_events != null)
                foreach (var @event in _events)
                    @event.TryInvoke();
        }
        internal GUID GUID { get; }

        // Initializers
        protected AQAnimation(float duration, AnimationCurve curve)
        {
            _duration = duration;
            _curve = curve;
        }
        protected AQAnimation(float duration, AnimationCurve curve, GUID guid)
            : this(duration, curve)
        {
            GUID = guid;
            _timeDeltaFunc = DefaultTimeDeltaFunc;
        }
        protected AQAnimation(float duration, AnimationCurve curve, TimeDeltaType timeDeltaType)
            : this(duration, curve)
        {
            GUID = DefaultGUID;
            _timeDeltaFunc = GetTimeDeltaFunc(timeDeltaType);
        }
        protected AQAnimation(float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
            : this(duration, curve)
        {
            GUID = DefaultGUID;
            _timeDeltaFunc = DefaultTimeDeltaFunc;
            _events = new HashSet<Event>();
            InitializeEvents(eventInfos);
        }
        protected AQAnimation(float duration, AnimationCurve curve, GUID guid, TimeDeltaType timeDeltaType)
            : this(duration, curve)
        {
            GUID = guid;
            _timeDeltaFunc = GetTimeDeltaFunc(timeDeltaType);
        }
        protected AQAnimation(float duration, AnimationCurve curve, GUID guid, IEnumerable<EventInfo> eventInfos)
            : this(duration, curve)
        {
            GUID = guid;
            _timeDeltaFunc = DefaultTimeDeltaFunc;
            _events = new HashSet<Event>();
            InitializeEvents(eventInfos);
        }
        protected AQAnimation(float duration, AnimationCurve curve, TimeDeltaType timeDeltaType, IEnumerable<EventInfo> eventInfos)
            : this(duration, curve)
        {
            GUID = DefaultGUID;
            _timeDeltaFunc = GetTimeDeltaFunc(timeDeltaType);
            _events = new HashSet<Event>();
            InitializeEvents(eventInfos);
        }
        protected AQAnimation(float duration, AnimationCurve curve, GUID guid, TimeDeltaType timeDeltaType, IEnumerable<EventInfo> eventInfos)
            : this(duration, curve)
        {
            GUID = guid;
            _timeDeltaFunc = GetTimeDeltaFunc(timeDeltaType);
            _events = new HashSet<Event>();
            InitializeEvents(eventInfos);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static private void StaticInitialize()
        {
            DefaultGUID = GUID.New;
            DefaultTimeDeltaFunc = () => Time.deltaTime;
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
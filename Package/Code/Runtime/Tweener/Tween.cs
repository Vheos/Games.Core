namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;
    using Tools.Extensions.Math;
    using Vheos.Tools.Extensions.UnityObjects;


    /// <summary> Represents a single animation, relative to a property's current state</summary>
    /// <remarks>
    ///     A single tween can modify multipile <c>UserProperties</c>, each with a different user function <br/>
    ///     A single <c>UserProperty</c> can be modified by multiple tweens <br/>
    ///     Blending <c>offset</c> (additive) and <c>ratio</c> (multiplicative) tweens is not recommended, as it will yield incosistent results <br/>
    ///     <br/>
    ///     To create a new tween, use either <c><see cref="New"/></c> or any of the <c><see cref="Tween_Extensions"/></c> <br/>
    /// </remarks>
    public class Tween
    {
        // Execution
        /// <summary> Creates a new, empty tween </summary>
        /// <remarks> 
        ///     The tween must be set up before it starts, which can be done in-line by chaining <c>Set_</c> and <c>Add_</c> methods <br/>
        ///     A tween without any property modifier can still be useful, to invoke conditional events or as a timer <br/>
        /// </remarks>
        static public Tween New
        => Tweener.NewTween;
        /// <summary> Instantly stops this tween </summary>
        /// <remarks> Does NOT apply the remaining delta value or call any events </remarks>
        public void Stop()
        => Tweener.StopTween(this);
        /// <summary> Instantly fast-forwards this tween to its end </summary>
        /// <remarks> Applies the remaining delta value and calls conditional and <c>OnFinish</c> events</remarks>
        public void Finish()
        {
            TrySetDefaults();

            _elapsed.Current = _duration.Value;
            _progress.Current = 1f;
            _curveValue.Current = _curveValueFunc(1f);

            _modifierFunctionInvoke?.Invoke(_curveValue.Current - _curveValue.Previous);

            if (_events != null)
                foreach (var @event in _events)
                    @event.TryInvoke();

            OnFinish?.Invoke();
            Stop();
        }
        /// <summary> Instantly stops all tweens on chosen layer </summary>
        static public void StopLayer(object conflictLayer)
        => Tweener.StopLayer(conflictLayer);

        // Settings
        /// <summary> Overrides <c>duration</c> </summary>
        /// <param name="duration"> </param>
        public Tween SetDuration(float duration)
        {
            _duration = duration;
            return this;
        }
        /// <summary> Overrides <c><see cref="AnimationCurve"/></c> </summary>
        /// <param name="curve"> </param>
        public Tween SetCurve(AnimationCurve curve)
        {
            _curve = curve;
            return this;
        }
        /// <summary> Overrides <c><see cref="CurveShape"/></c> </summary>
        /// <param name="curveShape"> </param>
        public Tween SetCurveShape(CurveShape curveShape)
        {
            _curveValueFunc = GetCurveValueFunc(curveShape);
            return this;
        }
        /// <summary> Overrides <c><see cref="DeltaTimeType"/></c> </summary>
        /// <param name="timeDeltaType"> </param>
        public Tween SetDeltaTime(DeltaTimeType timeDeltaType)
        {
            _deltaTimeFunc = GetDeltaTimeFunc(timeDeltaType);
            return this;
        }
        /// <summary> Overrides <c><see cref="UnityCore.ConflictResolution"/></c> </summary>
        /// <remarks> 
        ///     Controls how this tween affects ongoing tweens on the same layer <br/>
        ///     Has no effect is the tween is on a null conflict layer
        /// </remarks>  
        /// <param name="conflictResolution"> </param>
        public Tween SetConflictResolution(ConflictResolution conflictResolution)
        {
            ConflictResolution = conflictResolution;
            return this;
        }
        /// <summary> Moves this tween to the chosen conflict layer </summary>
        /// <remarks> 
        ///     If a tween is on a non-null conflict layer, it can affect ongoing tweens and be affected by new tweens on the same layer <br/>
        ///     You can choose how exactly it will affect ongoing tweens using <c><see cref="SetConflictResolution(ConflictResolution)"/></c> <br/>
        ///     Tweens with a null conflict layer don't affect and aren't affected by other tweens
        /// </remarks>
        /// <param name="conflictLayer">
        ///     Recommended assignments: <br/>
        ///     • instance of the tweened property's owner (per-instance layer)<br/>
        ///     • a tuple of the above and an enum/string (sublayer of a per-instance layer) <br/>
        ///     • a global enum/string identifier (global layer) <br/>
        ///     </param>
        public Tween SetConflictLayer(object conflictLayer)
        {
            ConflictLayer = conflictLayer;
            return this;
        }
        /// <summary> Sets reference <c><see cref="GameObject"/></c> for component-specific <c><see cref="Tween_Extensions"/></c> </summary>
        /// <param name="gameObject"> </param>
        public Tween SetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;
            return this;
        }
        /// <summary> Adds a modifier function for <c>UserProperty</c> </summary>
        /// <param name="modifierFunction">
        ///     This function modifies <c>UserProperty</c> on each tween update<br/>
        ///     Its input is the partial delta value (from last tween update):<br/>
        ///     • <c><see cref="DeltaValueType.Offset"/></c> - use additive assignment (<c>+=</c>)<br/>
        ///     • <c><see cref="DeltaValueType.Ratio"/></c> - use multiplicative assignment (<c>*=</c>)<br/>
        ///     <br/>
        ///     Recommended lamba expression format: <c>dV => <c>UserProperty</c> ?= dV</c><br/>
        /// </param>
        /// <param name="totalDelta">
        ///     Total change that will be applied to <c>UserProperty</c> over the course of the tween:<br/>
        ///     • <c><see cref="DeltaValueType.Offset"/></c> - <c><paramref name="totalDelta"/></c> will be added to <c>UserProperty</c><br/>
        ///     • <c><see cref="DeltaValueType.Ratio"/></c> - <c>UserProperty</c> will be multiplied by <c><paramref name="totalDelta"/></c><br/>
        /// </param>
        /// <param name="deltaType">
        ///     Controls how <c><paramref name="modifierFunction"/></c> and <c><paramref name="totalDelta"/></c> are interpreted<br/>
        ///     Check their respective documentations for more info on how <c><paramref name="deltaType"/></c> affects them
        /// </param>
        public Tween AddPropertyModifier<T>(Action<T> modifierFunction, T totalDelta, DeltaValueType deltaType = DeltaValueType.Offset) where T : struct
        {
            _modifierFunctionInvoke += GetModifierFunctionInvoke(modifierFunction, totalDelta, deltaType);
            return this;
        }
        /// <summary> Adds conditional events to this tween</summary>
        /// <param name="eventInfos"> Collection of <c><see cref="EventInfo"/></c>s that will be converted to internal conditional events </param>
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
                _events ??= new HashSet<ConditionalEvent>();
                _events.Add(new ConditionalEvent(eventInfo.Threshold, eventInfo.Action, GetEventValuePairFunc(eventInfo.ThresholdVariable)));
            }

            return this;
        }
        /// <summary> Adds events that will be invoked when this tween finishes </summary>
        /// <param name="onFinishEvents"> Collection of <c><see cref="Action"/></c>s to be invoked </param>
        public Tween AddOnFinishEvents(params Action[] onFinishEvents)
        {
            foreach (var @event in onFinishEvents)
                OnFinish += @event;

            return this;
        }

        // Defaults
        /// <summary> Default <c>duration</c> for all new tweens </summary>
        /// <remarks> Can be overriden per-tween with <c><see cref="SetDuration(float)"/></c> </remarks> 
        static public float DefaultDuration;
        /// <summary> Default <c><see cref="AnimationCurve"/></c> for all new tweens </summary>
        /// <remarks> Can be overriden per-tween with <c><see cref="SetCurve(AnimationCurve)"/></c> </remarks> 
        static public AnimationCurve DefaultCurve;
        /// <summary> Default <c><see cref="CurveShape"/></c> for all new tweens </summary>
        /// <remarks> Can be overriden per-tween with <c><see cref="SetCurveShape(CurveShape)"/></c> </remarks> 
        static public CurveShape DefaultCurveShape;
        /// <summary> Default <c><see cref="DeltaTimeType"/></c> for all new tweens </summary>
        /// <remarks> Can be overriden per-tween with <c><see cref="SetDeltaTime(DeltaTimeType)"/></c> </remarks> 
        static public DeltaTimeType DefaultDeltaTimeType;
        /// <summary> Default <c><see cref="UnityCore.ConflictResolution"/></c> for all new tweens </summary>
        /// <remarks> Can be overriden per-tween with <c><see cref="SetConflictResolution(UnityCore.ConflictResolution)"/></c> </remarks> 
        static public ConflictResolution DefaultConflictResolution;

        // Internals        
        internal void InvokeOnFinish()
        => OnFinish?.Invoke();
        internal bool HasFinished
        => _elapsed.Current >= _duration;
        internal bool IsOnAnyConflictLayer()
        => ConflictLayer != null;
        internal bool IsOnConflictLayer(object guid)
        => ConflictLayer == guid;
        internal void TrySetDefaults()
        {
            _duration ??= DefaultDuration;
            _curve ??= DefaultCurve;
            _curveValueFunc ??= GetCurveValueFunc(CurveShape.Normal);
            _deltaTimeFunc ??= GetDeltaTimeFunc(DeltaTimeType.Scaled);
            ConflictResolution ??= DefaultConflictResolution;
        }
        internal void Process()
        {
            UpdateElapsed(_deltaTimeFunc());
            UpdateProgress(_elapsed.Current);
            UpdateCurveValue(_progress.Current);

            _modifierFunctionInvoke?.Invoke(_curveValue.Current - _curveValue.Previous);

            if (_events != null)
                foreach (var @event in _events)
                    @event.TryInvoke();
        }

        // Settings
        private float? _duration;
        private AnimationCurve _curve;
        private Func<float, float> _curveValueFunc;
        private Func<float> _deltaTimeFunc;
        internal object ConflictLayer
        { get; private set; }
        internal ConflictResolution? ConflictResolution
        { get; private set; }
        internal GameObject GameObject
        { get; private set; }
        private Action<float> _modifierFunctionInvoke;
        private HashSet<ConditionalEvent> _events;
        private Action OnFinish;

        // Privates (helpers)
        private (float Current, float Previous) _elapsed, _progress, _curveValue;
        private void UpdateElapsed(float deltaTime)
        {
            _elapsed.Previous = _elapsed.Current;
            _elapsed.Current += deltaTime;
        }
        private void UpdateProgress(float elapsed)
        {
            _progress.Previous = _progress.Current;
            _progress.Current = elapsed.Div(_duration.Value).ClampMax(1f);
        }
        private void UpdateCurveValue(float progress)
        {
            _curveValue.Previous = _curveValue.Current;
            _curveValue.Current = _curveValueFunc(progress);
        }
        private Action<float> GetModifierFunctionInvoke<T>(Action<T> modifierFunction, T value, DeltaValueType deltaType) where T : struct
        {
            return deltaType switch
            {
                DeltaValueType.Offset => new GenericArgs<T>(modifierFunction, value) switch
                {
                    GenericArgs<float> t => dV => t.AssignFunc(t.Value * dV),
                    GenericArgs<Vector2> t => dV => t.AssignFunc(t.Value * dV),
                    GenericArgs<Vector3> t => dV => t.AssignFunc(t.Value * dV),
                    GenericArgs<Vector4> t => dV => t.AssignFunc(t.Value * dV),
                    GenericArgs<Color> t => dV => t.AssignFunc(t.Value * dV),
                    GenericArgs<Quaternion> t => dV => t.AssignFunc(Quaternion.identity.SLerp(t.Value, dV)),
                    _ => throw AnimationNotSupportedException<T>(deltaType),
                },
                DeltaValueType.Ratio => new GenericArgs<T>(modifierFunction, value) switch
                {
                    GenericArgs<float> t => dV => t.AssignFunc(t.Value.Pow(dV)),
                    GenericArgs<Vector2> t => dV => t.AssignFunc(t.Value.Pow(dV)),
                    GenericArgs<Vector3> t => dV => t.AssignFunc(t.Value.Pow(dV)),
                    GenericArgs<Vector4> t => dV => t.AssignFunc(t.Value.Pow(dV)),
                    GenericArgs<Color> t => dV => t.AssignFunc(t.Value.Pow(dV)),
                    _ => throw AnimationNotSupportedException<T>(deltaType),
                },
                _ => throw AnimationNotSupportedException<T>(deltaType),
            };
        }
        private Func<float, float> GetCurveValueFunc(CurveShape curveShape)
        => curveShape switch
        {
            CurveShape.Normal => p => _curve.Evaluate(p),
            CurveShape.Invert => p => 1f - _curve.Evaluate(1f - p),
            CurveShape.Mirror => p => _curve.Evaluate(2 * (p <= 0.5f ? p : 1f - p)),
            CurveShape.InvertAndMirror => p => 1f - _curve.Evaluate(1f - 2 * (p <= 0.5f ? p : 1f - p)),
            CurveShape.Bounce => p => 1f - (2 * _curve.Evaluate(p) - 1f).Abs(),
            CurveShape.InvertAndBounce => p => 1f - (2 * _curve.Evaluate(1f - p) - 1f).Abs(),
            _ => t => 0f,
        };
        private Func<float> GetDeltaTimeFunc(DeltaTimeType deltaType)
        => deltaType switch
        {
            DeltaTimeType.Scaled => () => Time.deltaTime,
            DeltaTimeType.Realtime => () => Time.unscaledDeltaTime,
            _ => () => default,
        };
        private NotSupportedException AnimationNotSupportedException<T>(DeltaValueType assignType) where T : struct
        => new NotSupportedException($"{assignType} {typeof(T).Name} animation is not supported!");

        // Initializers
        internal Tween()
        { }
        [SuppressMessage("CodeQuality", "IDE0051")]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static private void StaticInitialize()
        {
            DefaultDuration = 0.5f;
            DefaultCurve = new AnimationCurve();
            DefaultCurve.AddLinearKeys((0, 0), (1, 1));
            DefaultCurveShape = CurveShape.Normal;
            DefaultDeltaTimeType = DeltaTimeType.Scaled;
            DefaultConflictResolution = UnityCore.ConflictResolution.Blend;
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
        private class ConditionalEvent
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
            internal ConditionalEvent(float threshold, Action action, Func<(float, float)> valuePairFunc)
            {
                _threshold = threshold;
                _action = action;
                _valuePairFunc = valuePairFunc;
            }
        }
    }
}
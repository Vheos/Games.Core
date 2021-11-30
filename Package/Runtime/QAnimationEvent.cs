namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using ValuePair = System.ValueTuple<float, float>;

    public class QAnimationEvent
    {
        // Privates
        internal void TryInvoke()
        {
            if (Test())
                _action?.Invoke();
        }
        internal void ChooseValueFunction(Func<ValuePair> timeFunc, Func<ValuePair> progressFunc, Func<ValuePair> valueFunc)
        => _valuePairFunc = _thresholdType switch
        {
            AnimationEventThresholdType.Time => timeFunc,
            AnimationEventThresholdType.Progress => progressFunc,
            AnimationEventThresholdType.Value => valueFunc,
            _ => () => default,
        };
        private readonly AnimationEventThresholdType _thresholdType;
        private readonly float _threshold;
        private readonly Action _action;
        private Func<ValuePair> _valuePairFunc;
        private bool Test()
        {
            (float Current, float Previous) = _valuePairFunc();
            return Previous < _threshold && Current >= _threshold
                || Previous > _threshold && Current <= _threshold;
        }

        // Initializers
        public QAnimationEvent(AnimationEventThresholdType thresholdType, float threshold, Action action)
        {
            _thresholdType = thresholdType;
            _threshold = threshold;
            _action = action;
        }
    }

    public enum AnimationEventThresholdType
    {
        Time = 0,
        Progress,
        Value,
    }
}
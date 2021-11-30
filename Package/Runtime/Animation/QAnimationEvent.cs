namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    internal class QAnimationEvent
    {
        // Privates
        internal void TryInvoke()
        {
            if (Test())
                _action?.Invoke();
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
        internal QAnimationEvent(float threshold, Action action, Func<(float, float)> valuePairFunc)
        {
            _threshold = threshold;
            _action = action;
            _valuePairFunc = valuePairFunc;
        }
    }

}
namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;

    public class QAnimator : AManager<QAnimator>
    {
        // Publics
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType deltaType, AssignmentType assignType) where T : struct
        => _animations.Add(new QAnimation<T>(assignFunc, value, curve, duration, eventInfos, deltaType, assignType));
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration, IEnumerable<EventInfo> eventInfos, TimeDeltaType deltaType) where T : struct
        => _animations.Add(new QAnimation<T>(assignFunc, value, curve, duration, eventInfos, deltaType));
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration, IEnumerable<EventInfo> eventInfos) where T : struct
        => _animations.Add(new QAnimation<T>(assignFunc, value, curve, duration, eventInfos));
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration) where T : struct
        => _animations.Add(new QAnimation<T>(assignFunc, value, curve, duration));

        // Privates
        static private HashSet<AQAnimation> _animations;
        static private HashSet<AQAnimation> _finishedAnimations;
        static private void ProcessAnimations()
        {
            foreach (var animation in _animations)
            {
                animation.Process();
                if (animation.HasFinished)
                    _finishedAnimations.Add(animation);
            }

            if (_finishedAnimations.IsNotEmpty())
            {
                _animations.Remove(_finishedAnimations);
                _finishedAnimations.Clear();
            }
        }

        // Play
        protected override void DefineAutoSubscriptions()
        {
            base.DefineAutoSubscriptions();
            SubscribeTo(Get<Updatable>().OnUpdateLate, ProcessAnimations);
        }
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _animations = new HashSet<AQAnimation>();
            _finishedAnimations = new HashSet<AQAnimation>();
        }
    }
}
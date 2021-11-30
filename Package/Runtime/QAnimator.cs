namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.Math;

    public class QAnimator : AManager<QAnimator>
    {
        // Publics
        static public void Animate<T>(Action<T> assignFunc, AssignmentType assignType, T value, AnimationCurve curve, float duration, TimeDeltaType deltaType, IEnumerable<QAnimationEvent> events)
            where T : struct
        => _animations.Add(new QAnimation<T>(assignFunc, assignType, value, curve, duration, deltaType, events));

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
            SubscribeTo(Get<Updatable>().OnUpdate, ProcessAnimations);
        }
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _animations = new HashSet<AQAnimation>();
            _finishedAnimations = new HashSet<AQAnimation>();
        }
    }

    static public class QAnimator_Extensions
    {
        static private void AnimateCommon<T>(Action<T> assignFunc, AssignmentType assignType, T offset, float duration, IEnumerable<QAnimationEvent> events) where T : struct
        => QAnimator.Animate(assignFunc, assignType, offset, Qurve.ValuesByProgress, duration, TimeDeltaType.Scaled, events);
        static private void AnimatePositionCommon(Action<Vector3> assignFunc, Vector3 offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimateCommon(assignFunc, AssignmentType.Additive, offset, duration, events);
        static private void AnimateRotationCommon(Action<Quaternion> assignFunc, Quaternion offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimateCommon(assignFunc, AssignmentType.Additive, offset, duration, events);
        static private void AnimateRotationCommon(Action<Vector3> assignFunc, Vector3 offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimateCommon(assignFunc, AssignmentType.Additive, offset, duration, events);
        static private void AnimateScaleCommon(Action<Vector3> assignFunc, Vector3 offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimateCommon(assignFunc, AssignmentType.Multiplicative, offset, duration, events);

        static public void AnimatePosition(this Component t, Vector3 offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimatePositionCommon(v => t.transform.position += v, offset, duration, events);
        static public void AnimateLocalPosition(this Component t, Vector3 offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimatePositionCommon(v => t.transform.localPosition += v, offset, duration, events);


        static public void AnimateRotation(this Component t, Quaternion offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimateRotationCommon(v => t.transform.rotation = t.transform.rotation.Add(v), offset, duration, events);
        static public void AnimateLocalRotation(this Component t, Quaternion offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimateRotationCommon(v => t.transform.localRotation = t.transform.localRotation.Add(v), offset, duration, events);



        static public void AnimateRotation(this Component t, Vector3 offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimateRotationCommon(v => t.transform.rotation = t.transform.rotation.Add(v.ToRotation()), offset, duration, events);
        static public void AnimateLocalRotation(this Component t, Vector3 offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimateRotationCommon(v => t.transform.localRotation = t.transform.localRotation.Add(v.ToRotation()), offset, duration, events);


        static public void AnimateLocalScale(this Component t, Vector3 offset, float duration, IEnumerable<QAnimationEvent> events)
        => AnimateScaleCommon(v => t.transform.localScale = t.transform.localScale.Mul(v), offset, duration, events);
    }
}
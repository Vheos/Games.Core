namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;

    public class QAnimator : AManager<QAnimator>
    {
        // Publics
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration,
            IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType, AssignmentType assignType, ConflictResolution conflictResolution, object guid) where T : struct
        {
            QAnimation<T> createInvoke() => new QAnimation<T>(assignFunc, value, curve, duration, eventInfos, timeDeltaType, assignType, guid);
            if (_conflictAnimationGroupsByGUID.TryGet(guid, out var conflictAnim))
                switch (conflictResolution)
                {
                    case ConflictResolution.Blend:
                        AddConflictSensitiveAnimation(createInvoke());
                        break;
                    case ConflictResolution.Interrupt:
                        RemoveAnimation(conflictAnim);
                        AddConflictSensitiveAnimation(createInvoke());
                        break;
                    case ConflictResolution.Wait:
                        conflictAnim.OnHasFinished += () => Animate(assignFunc, value, curve, duration, eventInfos, timeDeltaType, assignType, conflictResolution, guid);
                        break;
                }
            else
                AddConflictSensitiveAnimation(createInvoke());
        }
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration,
            IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType, AssignmentType assignType) where T : struct
        => _animations.Add(new QAnimation<T>(assignFunc, value, curve, duration, eventInfos, timeDeltaType, assignType));
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration,
            IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType) where T : struct
        => _animations.Add(new QAnimation<T>(assignFunc, value, curve, duration, eventInfos, timeDeltaType));
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration,
            IEnumerable<EventInfo> eventInfos) where T : struct
        => _animations.Add(new QAnimation<T>(assignFunc, value, curve, duration, eventInfos));
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration)
            where T : struct
        => _animations.Add(new QAnimation<T>(assignFunc, value, curve, duration));

        // Privates
        static private HashSet<AQAnimation> _animations;
        static private Dictionary<object, HashSet<AQAnimation>> _conflictAnimationGroupsByGUID;
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
                foreach (var animation in _finishedAnimations)
                    RemoveAnimation(animation);
                _finishedAnimations.Clear();
            }
        }
        static private void AddConflictSensitiveAnimation(AQAnimation animation)
        {
            _animations.Add(animation);
            if (animation.GUID != null)
                _conflictAnimationGroupsByGUID.Add(animation.GUID, animation);
        }
        static private void RemoveAnimation(AQAnimation animation)
        {
            _animations.Remove(animation);
            _conflictAnimationGroupsByGUID.Remove(animation.GUID);
            animation.InvokeOnHasFinished();
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
            _conflictAnimationGroupsByGUID = new Dictionary<object, HashSet<AQAnimation>>();
            _finishedAnimations = new HashSet<AQAnimation>();
        }
    }
}
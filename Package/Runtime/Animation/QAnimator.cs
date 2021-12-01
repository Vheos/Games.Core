namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using System.Linq;

    public class QAnimator : AManager<QAnimator>
    {
        // Publics
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration,
            IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType, AssignmentType assignType, ConflictResolution conflictResolution, GUID guid) where T : struct
        {
            QAnimation<T> createInvoke() => new QAnimation<T>(assignFunc, value, curve, duration, eventInfos, timeDeltaType, assignType, guid);
            if (_animationGroupsByGUID.TryGet(guid, out var conflictAnimGroup))
                switch (conflictResolution)
                {
                    case ConflictResolution.Blend:
                        conflictAnimGroup.Add(createInvoke());
                        return;
                    case ConflictResolution.Interrupt:
                        conflictAnimGroup.Clear();
                        conflictAnimGroup.Add(createInvoke());
                        return;
                    case ConflictResolution.Wait:
                        if (conflictAnimGroup.IsEmpty())
                            conflictAnimGroup.Add(createInvoke());
                        else
                            conflictAnimGroup.First().OnHasFinished += () => Animate(assignFunc, value, curve, duration, eventInfos, timeDeltaType, assignType, conflictResolution, guid);
                        return;
                    default:
                        return;
                }

            _animationGroupsByGUID.Add(guid, new HashSet<AQAnimation>());
            _animationGroupsByGUID[guid].Add(createInvoke());
        }
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration,
            IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType, AssignmentType assignType) where T : struct
        => _animationGroupsByGUID[null].Add(new QAnimation<T>(assignFunc, value, curve, duration, eventInfos, timeDeltaType, assignType));
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration,
            IEnumerable<EventInfo> eventInfos, TimeDeltaType timeDeltaType) where T : struct
        => _animationGroupsByGUID[null].Add(new QAnimation<T>(assignFunc, value, curve, duration, eventInfos, timeDeltaType));
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration,
            IEnumerable<EventInfo> eventInfos) where T : struct
        => _animationGroupsByGUID[null].Add(new QAnimation<T>(assignFunc, value, curve, duration, eventInfos));
        static public void Animate<T>(Action<T> assignFunc, T value, AnimationCurve curve, float duration)
            where T : struct
        => _animationGroupsByGUID[null].Add(new QAnimation<T>(assignFunc, value, curve, duration));

        // Privates
        static private GUID _nonConflictGUID;
        static private Dictionary<GUID, HashSet<AQAnimation>> _animationGroupsByGUID;
        static private HashSet<AQAnimation> _finishedAnimations;
        static private void ProcessAnimations()
        {
            foreach (var animationGroupByGUID in _animationGroupsByGUID)
            {
                foreach (var animation in animationGroupByGUID.Value)
                {
                    animation.Process();
                    if (animation.HasFinished)
                        _finishedAnimations.Add(animation);
                }
            }

            if (_finishedAnimations.IsNotEmpty())
            {
                foreach (var animation in _finishedAnimations)
                    RemoveAnimation(animation);
                _finishedAnimations.Clear();
            }
        }
        static private void RemoveAnimation(AQAnimation animation)
        {
            _animationGroupsByGUID[animation.GUID].Remove(animation);
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
            _nonConflictGUID = GUID.New;
            _animationGroupsByGUID = new Dictionary<GUID, HashSet<AQAnimation>>
            {
                [_nonConflictGUID] = new HashSet<AQAnimation>()
            };
            _finishedAnimations = new HashSet<AQAnimation>();
        }
    }
}
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
        static public void Animate<T>(Action<T> assignFunc, T value, float duration)
            where T : struct
        {
            AddAnimation(new QAnimation<T>(duration, Qurve.ValuesByProgress, value, assignFunc, default));
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, AnimationCurve curve)
            where T : struct
        {
            AddAnimation(new QAnimation<T>(duration, curve, value, assignFunc, default));
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, IEnumerable<EventInfo> eventInfos)
            where T : struct
        {
            AddAnimation(new QAnimation<T>(duration, Qurve.ValuesByProgress, value, assignFunc, default, eventInfos));
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, GUID guid, ConflictResolution conflictResolution)
            where T : struct
        {
            QAnimation<T> createFunc() => new QAnimation<T>(duration, Qurve.ValuesByProgress, value, assignFunc, default, guid);
            void reinvokeAnimate() => Animate(assignFunc, value, duration, guid, conflictResolution);
            HandleConflict(guid, conflictResolution, createFunc, reinvokeAnimate);
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, AssignmentType assignType, TimeDeltaType timeDeltaType)
            where T : struct
        {
            AddAnimation(new QAnimation<T>(duration, Qurve.ValuesByProgress, value, assignFunc, assignType, timeDeltaType));
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos)
            where T : struct
        {
            AddAnimation(new QAnimation<T>(duration, curve, value, assignFunc, default, eventInfos));
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution)
            where T : struct
        {
            QAnimation<T> createFunc() => new QAnimation<T>(duration, curve, value, assignFunc, default, guid);
            void reinvokeAnimate() => Animate(assignFunc, value, duration, curve, guid, conflictResolution);
            HandleConflict(guid, conflictResolution, createFunc, reinvokeAnimate);
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, AnimationCurve curve, AssignmentType assignType, TimeDeltaType timeDeltaType)
            where T : struct
        {
            AddAnimation(new QAnimation<T>(duration, curve, value, assignFunc, assignType, timeDeltaType));
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
            where T : struct
        {
            QAnimation<T> createFunc() => new QAnimation<T>(duration, Qurve.ValuesByProgress, value, assignFunc, default, guid, eventInfos);
            void reinvokeAnimate() => Animate(assignFunc, value, duration, eventInfos, guid, conflictResolution);
            HandleConflict(guid, conflictResolution, createFunc, reinvokeAnimate);
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, IEnumerable<EventInfo> eventInfos, AssignmentType assignType, TimeDeltaType timeDeltaType)
           where T : struct
        {
            AddAnimation(new QAnimation<T>(duration, Qurve.ValuesByProgress, value, assignFunc, assignType, timeDeltaType, eventInfos));
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, GUID guid, ConflictResolution conflictResolution, AssignmentType assignType, TimeDeltaType timeDeltaType)
            where T : struct
        {
            QAnimation<T> createFunc() => new QAnimation<T>(duration, Qurve.ValuesByProgress, value, assignFunc, assignType, guid, timeDeltaType);
            void reinvokeAnimate() => Animate(assignFunc, value, duration, guid, conflictResolution, assignType, timeDeltaType);
            HandleConflict(guid, conflictResolution, createFunc, reinvokeAnimate);
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution)
           where T : struct
        {
            QAnimation<T> createFunc() => new QAnimation<T>(duration, curve, value, assignFunc, default, guid, eventInfos);
            void reinvokeAnimate() => Animate(assignFunc, value, duration, curve, eventInfos, guid, conflictResolution);
            HandleConflict(guid, conflictResolution, createFunc, reinvokeAnimate);
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, AssignmentType assignType, TimeDeltaType timeDeltaType)
           where T : struct
        {
            AddAnimation(new QAnimation<T>(duration, curve, value, assignFunc, assignType, timeDeltaType, eventInfos));
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, AnimationCurve curve, GUID guid, ConflictResolution conflictResolution, AssignmentType assignType, TimeDeltaType timeDeltaType)
            where T : struct
        {
            QAnimation<T> createFunc() => new QAnimation<T>(duration, curve, value, assignFunc, assignType, guid, timeDeltaType);
            void reinvokeAnimate() => Animate(assignFunc, value, duration, curve, guid, conflictResolution, assignType, timeDeltaType);
            HandleConflict(guid, conflictResolution, createFunc, reinvokeAnimate);
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, AssignmentType assignType, TimeDeltaType timeDeltaType)
            where T : struct
        {
            QAnimation<T> createFunc() => new QAnimation<T>(duration, Qurve.ValuesByProgress, value, assignFunc, assignType, guid, timeDeltaType, eventInfos);
            void reinvokeAnimate() => Animate(assignFunc, value, duration, eventInfos, guid, conflictResolution, assignType, timeDeltaType);
            HandleConflict(guid, conflictResolution, createFunc, reinvokeAnimate);
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, AnimationCurve curve, IEnumerable<EventInfo> eventInfos, GUID guid, ConflictResolution conflictResolution, AssignmentType assignType, TimeDeltaType timeDeltaType)
          where T : struct
        {
            QAnimation<T> createFunc() => new QAnimation<T>(duration, curve, value, assignFunc, assignType, guid, timeDeltaType, eventInfos);
            void reinvokeAnimate() => Animate(assignFunc, value, duration, curve, eventInfos, guid, conflictResolution, assignType, timeDeltaType);
            HandleConflict(guid, conflictResolution, createFunc, reinvokeAnimate);
        }

        // Privates
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
        static private void AddAnimation(AQAnimation animation)
        => _animationGroupsByGUID[animation.GUID].Add(animation);
        static private void RemoveAnimation(AQAnimation animation)
        {
            _animationGroupsByGUID[animation.GUID].Remove(animation);
            animation.InvokeOnHasFinished();
        }
        static private void HandleConflict<T>(GUID guid, ConflictResolution conflictResolution, Func<QAnimation<T>> createFunc, Action animateReinvoke)
            where T : struct
        {
            if (!_animationGroupsByGUID.TryGet(guid, out var conflictAnimGroup))
            {
                _animationGroupsByGUID.Add(guid, new HashSet<AQAnimation>());
                _animationGroupsByGUID[guid].Add(createFunc());
                return;
            }

            switch (conflictResolution)
            {
                case ConflictResolution.Blend:
                    conflictAnimGroup.Add(createFunc());
                    break;
                case ConflictResolution.Interrupt:
                    conflictAnimGroup.Clear();
                    conflictAnimGroup.Add(createFunc());
                    break;
                case ConflictResolution.Wait when conflictAnimGroup.IsEmpty():
                    conflictAnimGroup.Add(createFunc());
                    break;
                case ConflictResolution.Wait when conflictAnimGroup.IsNotEmpty():
                    conflictAnimGroup.First().OnHasFinished += animateReinvoke;
                    break;
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
            _animationGroupsByGUID = new Dictionary<GUID, HashSet<AQAnimation>>
            {
                [AQAnimation.DefaultGUID] = new HashSet<AQAnimation>()
            };
            _finishedAnimations = new HashSet<AQAnimation>();
        }

#if UNITY_EDITOR
        // Debug
        [ContextMenu(nameof(PrintDebugInfo))]
        private void PrintDebugInfo()
        {
            Debug.Log($"{nameof(_animationGroupsByGUID)} ({_animationGroupsByGUID.Count})");
            foreach (var animationGroupByGUID in _animationGroupsByGUID)
            {
                Debug.Log($"\t• {animationGroupByGUID.Key.GetHashCode():X} ({animationGroupByGUID.Value.Count})");
                foreach (var animation in animationGroupByGUID.Value)
                    Debug.Log($"\t\t- {animation.GetType().GetGenericArguments().First().Name}");
            }
            Debug.Log($"");
        }
#endif
    }
}
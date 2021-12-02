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
            if (duration <= 0)
                assignFunc(value);
            else
                AddAnimation(new QAnimation<T>(assignFunc, value, duration));
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, OptionalParameters optionals)
            where T : struct
        {
            void addAnimInvoke()
            {
                if (duration <= 0)
                    assignFunc(value);
                else
                    AddAnimation(new QAnimation<T>(assignFunc, value, duration, optionals));
            }

            if (optionals.ConflictResolution == null || optionals.GUID == null)
            {
                addAnimInvoke();
                return;
            }
            if (!_animationGroupsByGUID.TryGet(optionals.GUID, out var conflictAnimGroup))
            {
                _animationGroupsByGUID.Add(optionals.GUID, new HashSet<AQAnimation>());
                addAnimInvoke();
                return;
            }
            switch (optionals.ConflictResolution)
            {
                case ConflictResolution.Blend:
                    addAnimInvoke();
                    break;
                case ConflictResolution.Interrupt:
                    conflictAnimGroup.Clear();
                    addAnimInvoke();
                    break;
            }
        }
        static public void Stop(GUID guid)
        => _animationGroupsByGUID[guid].Clear();

        // Privates
        static internal OptionalParameters OptionalsMultiplicative
        { get; private set; }
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

        // Play
        protected override void DefineAutoSubscriptions()
        {
            base.DefineAutoSubscriptions();
            SubscribeTo(Get<Updatable>().OnUpdateLate, ProcessAnimations);
        }
        protected override void PlayAwake()
        {
            base.PlayAwake();
            OptionalsMultiplicative = new OptionalParameters { AssignmentType = AssignmentType.Multiplicative };
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
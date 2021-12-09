namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;

    public class QAnimator : AManager<QAnimator>
    {
        // Publics
        static public void Animate<T>(Action<T> assignFunc, T value, float duration)
            where T : struct
        {
            if (duration <= 0)
            {
                assignFunc(value);
                return;
            }

            QAnimation newAnimation = new QAnimation(duration);
            _animations.Add(newAnimation);
            newAnimation.AddAssignment(assignFunc, value, AssignmentType.Additive);
        }
        static public void Animate<T>(Action<T> assignFunc, T value, float duration, OptionalParameters optionals)
            where T : struct
        {
            void CreateAnimFunc()
            {
                if (duration <= 0)
                {
                    assignFunc(value);
                    TryInvokeOnHasFinishedEvents(optionals.EventInfo);
                    return;
                }

                QAnimation newAnimation = new QAnimation(duration, optionals);
                newAnimation.AddAssignment(assignFunc, value, optionals.AssignmentType ?? AssignmentType.Additive);
                _animations.Add(newAnimation);
            }

            TryResolveConflict(CreateAnimFunc, optionals.ConflictResolution, optionals.GUID);
        }
        static public void Delay(float duration)
        {
            if (duration <= 0)
                return;

            _animations.Add(new QAnimation(duration));
        }
        static public void Delay(float duration, OptionalParameters optionals)
        {
            void CreateAnimFunc()
            {
                if (duration <= 0)
                {
                    TryInvokeOnHasFinishedEvents(optionals.EventInfo);
                    return;
                }

                _animations.Add(new QAnimation(duration, optionals));
            }

            TryResolveConflict(CreateAnimFunc, optionals.ConflictResolution, optionals.GUID);
        }
        static public void Stop(object guid)
        => _animations.RemoveWhere(t => t.GUID == guid);

        // Publics (group)
        static public IDisposable Group(float duration)
        {
            _isInGroupBlock = true;
            _groupAnimation = new QAnimation(duration);
            _groupAssignmentType = AssignmentType.Additive;
            return _groupDisposable;
        }
        static public IDisposable Group(float duration, OptionalParameters optionals)
        {
            _isInGroupBlock = true;
            void InitializeGroup()
            {
                _groupAnimation = new QAnimation(duration, optionals);
                _groupAssignmentType = optionals.AssignmentType;
            }

            TryResolveConflict(InitializeGroup, optionals.ConflictResolution, optionals.GUID);
            return _groupDisposable;
        }
        static public void GroupAnimate<T>(Action<T> assignFunc, T value) where T : struct
        => GroupAnimate(assignFunc, value, _groupAssignmentType ?? AssignmentType.Additive);
        static public void GroupAnimate<T>(Action<T> assignFunc, T value, AssignmentType assignType) where T : struct
        {
            if (!_isInGroupBlock && WarningOutsideGroupBlock()
            || _groupAnimation == null)
                return;

            if(_groupAnimation.IsInstant)
            {
                assignFunc(value);
                return;
            }

            _groupAnimation.AddAssignment(assignFunc, value, assignType);
        }

        // Privates
        static private HashSet<QAnimation> _animations;
        static private HashSet<QAnimation> _pendingRemoves;
        static private void ProcessAnimations()
        {
            foreach (var animation in _animations)
            {
                animation.Process();
                if (animation.HasFinished)
                    _pendingRemoves.Add(animation);
            }

            if (_pendingRemoves.IsNotEmpty())
            {
                foreach (var animation in _pendingRemoves)
                {
                    _animations.Remove(animation);
                    animation.InvokeOnHasFinished();
                }
                _pendingRemoves.Clear();
            }
        }
        static private void TryResolveConflict(Action createAnim, ConflictResolution? conflictResolution, object guid)
        {
            if (conflictResolution == null || guid == null)
                createAnim();
            else
                switch (conflictResolution)
                {
                    case ConflictResolution.Blend:
                        createAnim();
                        break;
                    case ConflictResolution.Interrupt:
                        Stop(guid);
                        createAnim();
                        break;
                    case ConflictResolution.Wait:
                        // TO DO
                        break;
                    case ConflictResolution.DoNothing:
                        break;
                }
        }
        static private void TryInvokeOnHasFinishedEvents(EventInfo[] eventInfo)
        {
            if (eventInfo == null)
                return;

            foreach (var info in eventInfo)
                if (info.IsOnHasFinished)
                    info.Action();
        }

        // Privates (group)
        static private CustomDisposable _groupDisposable;
        static private bool _isInGroupBlock;
        static private AssignmentType? _groupAssignmentType;
        static private QAnimation _groupAnimation;
        static private void FinalizeGroup()
        {
            if (_groupAnimation.HasAnyAssignments)
                _animations.Add(_groupAnimation);

            _isInGroupBlock = false;
            _groupAssignmentType = null;
            _groupAnimation = null;
        }
        static private bool WarningOutsideGroupBlock()
        {
            Debug.LogWarning($"AnimationOutsideGroupBlock:\ttrying to GroupAnimate outside the using(QAnimator.Group) block\n" +
            $"Fallback:\treturn without creating the animation");
            return true;
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
            _animations = new HashSet<QAnimation>();
            _pendingRemoves = new HashSet<QAnimation>();
            _groupDisposable = new CustomDisposable(FinalizeGroup);
        }

#if UNITY_EDITOR
        // Debug
        [ContextMenu(nameof(PrintDebugInfo))]
        private void PrintDebugInfo()
        {
            Debug.Log($"ANIMATIONS: ({_animations.Count})");
            foreach (var animation in _animations)
                Debug.Log($"\t• {animation.GetHashCode():X}{(animation.GUID != null ? " (conflict)" : "")}");
            Debug.Log($"");
        }
#endif
    }
}
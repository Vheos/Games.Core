namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;

    public class QAnimator : AManager<QAnimator>
    {
        // Publics
        static public QAnimation Animate(float duration)
        {
            QAnimation newAnimation = new QAnimation(duration);
            _pendingAnims.Add(newAnimation);
            return newAnimation;
        }
        static public void Stop(object guid)
        => _playingAnims.RemoveWhere(t => t.HasGUID(guid));

        // Privates
        static private HashSet<QAnimation> _pendingAnims;
        static private HashSet<QAnimation> _playingAnims;
        static private HashSet<QAnimation> _finishedAnims;
        static private void ProcessPendingAnimations()
        {
            if (_pendingAnims.IsNotEmpty())
            {
                static void Process(QAnimation animation)
                {
                    animation.InitializeLate();
                    if (animation.HasFinished)
                        animation.InstantFinish();
                    else
                        _playingAnims.Add(animation);
                }

                foreach (var animation in _pendingAnims)
                    if (animation.HasGUID())
                        ResolveConflict(animation, Process);
                    else
                        Process(animation);

                _pendingAnims.Clear();
            }
        }
        static private void ProcessPlayingAnimations()
        {
            foreach (var animation in _playingAnims)
            {
                animation.Process();
                if (animation.HasFinished)
                    _finishedAnims.Add(animation);
            }
        }
        static private void ProcessFinishedAnimations()
        {
            if (_finishedAnims.IsNotEmpty())
            {
                foreach (var animation in _finishedAnims)
                {
                    _playingAnims.Remove(animation);
                    animation.InvokeOnFinish();
                }
                _finishedAnims.Clear();
            }
        }
        static private void ResolveConflict(QAnimation animation, Action<QAnimation> processFunc)
        {
            switch (animation.ConflictResolution)
            {
                case ConflictResolution.Blend:
                    processFunc(animation);
                    break;
                case ConflictResolution.Interrupt:
                    Stop(animation.GUID);
                    processFunc(animation);
                    break;
                case ConflictResolution.Wait:
                    // TO DO
                    break;
                case ConflictResolution.DoNothing:
                    break;
            }
        }

        // Play
        protected override void DefineAutoSubscriptions()
        {
            base.DefineAutoSubscriptions();
            SubscribeTo(Get<Updatable>().OnUpdateLate, ProcessPendingAnimations);
            SubscribeTo(Get<Updatable>().OnUpdateLate, ProcessPlayingAnimations);
            SubscribeTo(Get<Updatable>().OnUpdateLate, ProcessFinishedAnimations);
        }
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _playingAnims = new HashSet<QAnimation>();
            _pendingAnims = new HashSet<QAnimation>();
            _finishedAnims = new HashSet<QAnimation>();
        }

#if UNITY_EDITOR
        // Debug
        [ContextMenu(nameof(PrintDebugInfo))]
        private void PrintDebugInfo()
        {
            Debug.Log($"ANIMATIONS: ({_playingAnims.Count})");
            foreach (var animation in _playingAnims)
                Debug.Log($"\t• {animation.GetHashCode():X}{(animation.HasGUID() ? " [GUID]" : "")}");
            Debug.Log($"");
        }
#endif
    }
}
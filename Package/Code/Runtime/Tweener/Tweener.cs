namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using System.Linq;

    internal class Tweener : AAutoSubscriber
    {
        // Internals
        static internal Tween NewTween
        {
            get
            {
                Tween newTween = new Tween();
                _pendingTweens.Add(newTween);
                return newTween;
            }
        }
        static internal void StopTween(Tween tween)
        {
            _pendingTweens.Remove(tween);
            _playingTweens.Remove(tween);
        }
        static internal void StopLayer(object conflictLayer)
        {
            _pendingTweens.RemoveWhere(t => t.IsOnConflictLayer(conflictLayer));
            _playingTweens.RemoveWhere(t => t.IsOnConflictLayer(conflictLayer));
        }

        // Privates
        static private HashSet<Tween> _pendingTweens;
        static private HashSet<Tween> _playingTweens;
        static private HashSet<Tween> _finishedTweens;
        static private void ProcessPendingTweens()
        {
            foreach (var tween in _pendingTweens)
            {
                tween.TrySetDefaults();
                if (tween.IsOnAnyConflictLayer())
                    ResolveConflict(tween);
                else
                    _playingTweens.Add(tween);
            }

            if (_pendingTweens.IsNotEmpty())
                _pendingTweens.Clear();
        }
        static private void ProcessPlayingTweens()
        {
            foreach (var tween in _playingTweens)
            {
                tween.Process();
                if (tween.HasFinished)
                    _finishedTweens.Add(tween);
            }
        }
        static private void ProcessFinishedTweens()
        {
            foreach (var tween in _finishedTweens)
            {
                tween.InvokeOnFinish();
                _playingTweens.Remove(tween);
            }

            if (_finishedTweens.IsNotEmpty())
                _finishedTweens.Clear();

        }
        static private void ResolveConflict(Tween tween)
        {
            switch (tween.ConflictResolution)
            {
                case ConflictResolution.Blend:
                    _playingTweens.Add(tween);
                    break;
                case ConflictResolution.Interrupt:
                    _playingTweens.RemoveWhere(t => t.IsOnConflictLayer(tween.ConflictLayer));
                    _playingTweens.Add(tween);
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
            SubscribeAuto(Get<Updatable>().OnUpdateLate, ProcessPendingTweens);
            SubscribeAuto(Get<Updatable>().OnUpdateLate, ProcessPlayingTweens);
            SubscribeAuto(Get<Updatable>().OnUpdateLate, ProcessFinishedTweens);
        }
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _playingTweens = new HashSet<Tween>();
            _pendingTweens = new HashSet<Tween>();
            _finishedTweens = new HashSet<Tween>();
        }

#if UNITY_EDITOR
        // Debug
        [ContextMenu(nameof(PrintDebugInfo))]
        private void PrintDebugInfo()
        {
            Debug.Log($"ANIMATIONS: ({_playingTweens.Count})");
            foreach (var tween in _playingTweens)
                Debug.Log($"\t• {tween.GetHashCode():X}{(tween.IsOnAnyConflictLayer() ? " [GUID]" : "")}");
            Debug.Log($"");
        }
#endif
    }
}
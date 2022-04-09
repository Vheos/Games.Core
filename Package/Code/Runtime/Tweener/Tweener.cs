namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;
    using Tools.Extensions.Collections;

    [RequireComponent(typeof(Updatable))]
    internal class Tweener : ABaseComponent
    {
        // Internals
        static internal Tween NewTween
        {
            get
            {
                Tween newTween = new();
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
        static internal void StopGameObject(GameObject gameObject)
        {
            _pendingTweens.RemoveWhere(t => t.GameObject == gameObject);
            _playingTweens.RemoveWhere(t => t.GameObject == gameObject);
        }
        static internal void StopAll()
        {
            _pendingTweens.Clear();
            _playingTweens.Clear();
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
        protected override void PlayAwake()
        {
            base.PlayAwake();
            Get<Updatable>().OnUpdateLate.SubEnableDisable(this, ProcessPendingTweens, ProcessPlayingTweens, ProcessFinishedTweens);
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static private void StaticInitialize()
        {
            _playingTweens = new();
            _pendingTweens = new();
            _finishedTweens = new();
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
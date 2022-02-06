namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Expandable : ABaseComponent
    {
        // Events   
        public readonly AutoEvent OnStartExpanding = new();
        public readonly AutoEvent OnFinishExpanding = new();
        public readonly AutoEvent OnStartCollapsing = new();
        public readonly AutoEvent OnFinishCollapsing = new();

        // Getters
        public readonly Getter<Tween> ExpandTween = new();
        public readonly Getter<Tween> CollapseTween = new();

        // Publics
        public ExpandableState State
        { get; private set; }
        public bool CanExpand
        => State != ExpandableState.Expanded && State != ExpandableState.Expanding;
        public bool CanCollapse
        => State != ExpandableState.Collapsed && State != ExpandableState.Collapsing;
        public bool TryExpand(bool instantly = false)
        {
            if (!CanExpand)
                return false;

            State = ExpandableState.Expanding;
            OnStartExpanding.Invoke();
            ExpandTween.Value
                .SetConflictLayer(this)
                .SetConflictResolution(ConflictResolution.Interrupt)
                .OnFinish(() => State = ExpandableState.Expanded, () => OnFinishExpanding.Invoke())
                .FinishIf(instantly);
            return true;
        }
        public bool TryCollapse(bool instantly = false)
        {
            if (!CanCollapse)
                return false;

            State = ExpandableState.Collapsing;
            OnStartCollapsing.Invoke();
            CollapseTween.Value
                .SetConflictLayer(this)
                .SetConflictResolution(ConflictResolution.Interrupt)
                .OnFinish(() => State = ExpandableState.Collapsed, () => OnFinishCollapsing.Invoke())
                .FinishIf(instantly);
            return true;
        }
        public void Toggle(bool instantly = false)
        {
            if (!TryExpand(instantly))
                TryCollapse(instantly);
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            ExpandTween.Set(() => Tween.New);
            CollapseTween.Set(() => Tween.New);
        }
    }
}
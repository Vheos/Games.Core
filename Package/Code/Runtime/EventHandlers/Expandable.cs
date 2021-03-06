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
        public readonly Getter<Tween> ExpandTween = new(() => Tween.New);
        public readonly Getter<Tween> CollapseTween = new(() => Tween.New);

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
                .SetInterrupt()
                .AddEventsOnFinish(() => State = ExpandableState.Expanded, () => OnFinishExpanding.Invoke())
                .If(instantly).Finish();
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
                .SetInterrupt()
                .AddEventsOnFinish(() => State = ExpandableState.Collapsed, () => OnFinishCollapsing.Invoke())
                .If(instantly).Finish();
            return true;
        }
        public void Toggle(bool instantly = false)
        {
            if (!TryExpand(instantly))
                TryCollapse(instantly);
        }
    }
}
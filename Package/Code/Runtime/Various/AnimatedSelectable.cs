namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Games.Core;
    using Tools.Extensions.Math;

    [RequireComponent(typeof(Selectable))]
    public class AnimatedSelectable : ABaseComponent
    {
        // New
        [field: SerializeField, Range(0f, 2f)] public float SelectDuration { get; private set; } = 0.4f;
        [field: SerializeField, Range(0f, 2f)] public float DeselectDuration { get; private set; } = 0.8f;
        [field: SerializeField, Range(1f, 2f)] public float SelectScale { get; private set; } = 1.1f;
        [field: SerializeField, Range(1f, 2f)] public float SelectColorScale { get; private set; } = 1f;

        [field: SerializeField, Range(0f, 2f)] public float PressDuration { get; private set; } = 0.1f;
        [field: SerializeField, Range(0f, 2f)] public float ReleaseDuration { get; private set; } = 0.2f;
        [field: SerializeField, Range(0.5f, 2f)] public float PressScale { get; private set; } = 0.95f;
        [field: SerializeField, Range(0.5f, 2f)] public float PressColorScale { get; private set; } = 0.8f;

        // Publics
        public void Pause()
        {
            if (_isPaused)
                return;

            _isPaused = true;
            _pausedState = _currentState;
        }
        public void Unpause()
        {
            if (!_isPaused)
                return;

            _isPaused = false;
            State stateDiff = _pausedState ^ _currentState;

            if (stateDiff.HasFlag(State.Selected))
                if (_currentState.HasFlag(State.Selected))
                    AnimateSelected();
                else
                    AnimateDeselected();

            if (stateDiff.HasFlag(State.Pressed))
                if (_currentState.HasFlag(State.Pressed))
                    AnimatePressed();
                else
                    AnimateReleased();
        }
        public void UpdateColorComponent()
        => _colorComponent = this.FindColorComponent();

        // Privates
        private ColorComponent _colorComponent;
        private State _pausedState;
        private State _currentState;
        private bool _isPaused;
        private void Selectable_OnGetSelected(Selectable selectable, Selecter selecter)
        {
            if (selectable.IsSelectedByMany)
                return;

            _currentState |= State.Selected;
            if (_isPaused)
                return;

            AnimateSelected();
        }
        private void Selectable_OnGetDeselected(Selectable selectable, Selecter selecter)
        {
            if (selectable.IsSelected)
                return;

            _currentState &= ~State.Selected;
            if (_isPaused)
                return;

            AnimateDeselected();
        }
        private void Selectable_OnGetPressed(Selectable selectable, Selecter selecter)
        {
            _currentState |= State.Pressed;
            if (_isPaused)
                return;

            AnimatePressed();
        }
        private void Selectable_OnGetReleased(Selectable selectable, Selecter selecter, bool isClick)
        {
            _currentState &= ~State.Pressed;
            if (_isPaused)
                return;

            AnimateReleased();
        }
        private void AnimateSelected()
        => this.NewTween()
            .SetDuration(SelectDuration)
            .If(SelectScale != 1f).LocalScaleRatio(SelectScale)
            .If(SelectColorScale != 1f).RGBRatio(_colorComponent, SelectColorScale);
        private void AnimateDeselected()
        => this.NewTween()
            .SetDuration(DeselectDuration)
            .If(SelectScale != 1f).LocalScaleRatio(SelectScale.Inv())
            .If(SelectColorScale != 1f).RGBRatio(_colorComponent, SelectColorScale.Inv());
        private void AnimatePressed()
        => this.NewTween()
            .SetDuration(PressDuration)
            .If(PressScale != 1f).LocalScaleRatio(PressScale)
            .If(PressColorScale != 1f).RGBRatio(_colorComponent, PressColorScale);
        private void AnimateReleased()
        => this.NewTween()
            .SetDuration(ReleaseDuration)
            .If(PressScale != 1f).LocalScaleRatio(PressScale.Inv())
            .If(PressColorScale != 1f).RGBRatio(_colorComponent, PressColorScale.Inv());

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            UpdateColorComponent();

            Get<Selectable>().OnGetSelected.SubEnableDisable(this, Selectable_OnGetSelected);
            Get<Selectable>().OnGetDeselected.SubEnableDisable(this, Selectable_OnGetDeselected);
            Get<Selectable>().OnGetPressed.SubEnableDisable(this, Selectable_OnGetPressed);
            Get<Selectable>().OnGetReleased.SubEnableDisable(this, Selectable_OnGetReleased);
        }

        // Defines
        [Flags]
        private enum State
        {
            Selected = 1 << 0,
            Pressed = 1 << 1,
        }
    }
}
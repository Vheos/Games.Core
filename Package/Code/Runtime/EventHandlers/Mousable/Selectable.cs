namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;

    [DisallowMultipleComponent]
    sealed public class Selectable : ABaseComponent
    {
        // Events
        public AutoEvent OnGainHighlight
        { get; } = new AutoEvent();
        public AutoEvent OnLoseHighlight
        { get; } = new AutoEvent();
        public AutoEvent<ButtonFunction> OnPress
        { get; } = new AutoEvent<ButtonFunction>();
        public AutoEvent<ButtonFunction> OnHold
        { get; } = new AutoEvent<ButtonFunction>();
        public AutoEvent<ButtonFunction, bool> OnRelease
        { get; } = new AutoEvent<ButtonFunction, bool>();

        // Getters
        static public Getter<KeyCode, ButtonFunction> KeyCodeToButtonFunction
        { get; private set; }

        // Privates
        internal void GainHighlight()
        => OnGainHighlight?.Invoke();
        internal void LoseHighlight()
        => OnLoseHighlight?.Invoke();
        internal void Press(ButtonFunction function)
        => OnPress?.Invoke(function);
        internal void Hold(ButtonFunction function)
        => OnHold?.Invoke(function);
        internal void Release(ButtonFunction function, bool isClick)
        => OnRelease?.Invoke(function, isClick);

        // Initializers
        [SuppressMessage("CodeQuality", "IDE0051")]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static private void StaticInitialize()
        => KeyCodeToButtonFunction = new Getter<KeyCode, ButtonFunction>();
    }

    public enum ButtonFunction
    {
        None,
        Primary,
        Secondary,
        Tertiary,
    }

    static public class Selectable_Extensions
    {
        static public ButtonFunction ToFunction(this KeyCode keyCode)
        => Selectable.KeyCodeToButtonFunction[keyCode];
    }
}
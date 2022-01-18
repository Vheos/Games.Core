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
        internal void InvokeOnGainHighlight()
        => OnGainHighlight?.Invoke();
        internal void InvokeOnLoseHighlight()
        => OnLoseHighlight?.Invoke();
        internal void InvokeOnPress(ButtonFunction function)
        => OnPress?.Invoke(function);
        internal void InvokeOnHold(ButtonFunction function)
        => OnHold?.Invoke(function);
        internal void InvokeOnRelease(ButtonFunction function, bool isFullClick)
        => OnRelease?.Invoke(function, isFullClick);

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
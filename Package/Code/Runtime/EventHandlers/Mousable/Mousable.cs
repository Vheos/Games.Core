namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    public class Mousable : AAutoSubscriber
    {
        // Events
        public AutoEvent OnGainHighlight
        { get; } = new AutoEvent();
        public AutoEvent OnLoseHighlight
        { get; } = new AutoEvent();
        public AutoEvent<MouseButton, Vector3> OnPress
        { get; } = new AutoEvent<MouseButton, Vector3>();
        public AutoEvent<MouseButton, Vector3> OnHold
        { get; } = new AutoEvent<MouseButton, Vector3>();
        public AutoEvent<MouseButton, bool> OnRelease
        { get; } = new AutoEvent<MouseButton, bool>();

        // Publics
        public Collider Trigger
        { get; private set; }
        public event Func<Vector3, bool> RaycastTests;
        public bool PerformRaycastTests(Vector3 position)
        {
            if (RaycastTests != null)
                foreach (Func<Vector3, bool> raycastTest in RaycastTests.GetInvocationList())
                    if (!raycastTest(position))
                        return false;
            return true;
        }

        // Privates
        private void AssignLayer()
        => gameObject.layer = LayerMask.NameToLayer(nameof(Mousable));
        private protected void TryFitBoxColliderToMesh()
        {
            if (Trigger.TryAs(out BoxCollider boxCollider)
            && TryGet(out MeshFilter meshFilter))
                boxCollider.size = meshFilter.mesh.bounds.size;
        }
        private void TryInvokeSelectableOnPress(Selectable selectable, MouseButton button)
        {
            ButtonFunction function = button.ToKeyCode().ToFunction();
            if (function != ButtonFunction.None)
                selectable.OnPress.Invoke(function);
        }
        private void TryInvokeSelectableOnHold(Selectable selectable, MouseButton button)
        {
            ButtonFunction function = button.ToKeyCode().ToFunction();
            if (function != ButtonFunction.None)
                selectable.OnHold.Invoke(function);
        }
        private void TryInvokeSelectableOnRelease(Selectable selectable, MouseButton button, bool isClick)
        {
            ButtonFunction function = button.ToKeyCode().ToFunction();
            if (function != ButtonFunction.None)
                selectable.OnRelease.Invoke(function, isClick);
        }

        // Play
        protected override void DefineAutoSubscriptions()
        {
            base.DefineAutoSubscriptions();
            if (TryGet(out Selectable selectable))
            {
                SubscribeAuto(OnGainHighlight, selectable.OnGainHighlight.Invoke);
                SubscribeAuto(OnLoseHighlight, selectable.OnLoseHighlight.Invoke);
                SubscribeAuto(OnPress, (button, position) => TryInvokeSelectableOnPress(selectable, button));
                SubscribeAuto(OnHold, (button, position) => TryInvokeSelectableOnHold(selectable, button));
                SubscribeAuto(OnRelease, (button, isClick) => TryInvokeSelectableOnRelease(selectable, button, isClick));
            }
        }
        protected override void PlayAwake()
        {
            base.PlayAwake();
            Trigger = Get<Collider>();
            TryFitBoxColliderToMesh();
            AssignLayer();
        }

#if CACHED_COMPONENTS
        protected override void DefineCachedComponents()
        {
            base.DefineCachedComponents();
            TryAddToCache<Collider>();
        }
#endif
    }

    // Defines
    public enum MouseButton
    {
        None,
        Left,
        Right,
        Middle,
        Extra1,
        Extra2,
        Extra3,
        Extra4,
    }

    static public class Mousable_Extensions
    {
        static public MouseButton ToMouseButton(this KeyCode keyCode)
        => keyCode switch
        {
            KeyCode.Mouse0 => MouseButton.Left,
            KeyCode.Mouse1 => MouseButton.Right,
            KeyCode.Mouse2 => MouseButton.Middle,
            KeyCode.Mouse3 => MouseButton.Extra1,
            KeyCode.Mouse4 => MouseButton.Extra2,
            KeyCode.Mouse5 => MouseButton.Extra3,
            KeyCode.Mouse6 => MouseButton.Extra4,
            _ => MouseButton.None,
        };
        static public KeyCode ToKeyCode(this MouseButton mouseButton)
        => mouseButton switch
        {
            MouseButton.Left => KeyCode.Mouse0,
            MouseButton.Right => KeyCode.Mouse1,
            MouseButton.Middle => KeyCode.Mouse2,
            MouseButton.Extra1 => KeyCode.Mouse3,
            MouseButton.Extra2 => KeyCode.Mouse4,
            MouseButton.Extra3 => KeyCode.Mouse5,
            MouseButton.Extra4 => KeyCode.Mouse6,
            _ => KeyCode.None,
        };
    }
}

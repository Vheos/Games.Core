namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Tools.Extensions.General;
    using Vheos.Tools.Extensions.UnityObjects;

    [RequireComponent(typeof(Collider))]
    [DisallowMultipleComponent]
    public class UICursorable : AAutoSubscriber
    {
        // Eventsd
        public AutoEvent OnGainHighlight
        { get; } = new AutoEvent();
        public AutoEvent OnLoseHighlight
        { get; } = new AutoEvent();
        public AutoEvent<Vector3> OnPress
        { get; } = new AutoEvent<Vector3>();
        public AutoEvent<Vector3> OnHold
        { get; } = new AutoEvent<Vector3>();
        public AutoEvent<bool> OnRelease
        { get; } = new AutoEvent<bool>();

        // Publics
        public Collider Trigger
        { get; private set; }
        public bool PerformRaycastTests(Vector3 position)
        {
            if (_raycastTests != null)
                foreach (Func<Vector3, bool> test in _raycastTests)
                    if (!test(position))
                        return false;
            return true;
        }
        public void AddRaycastTest(Func<Vector3, bool> test)
        => _raycastTests.Add(test);

        // Privates
        private HashSet<Func<Vector3, bool>> _raycastTests;
        private RaycastTargetType _raycastTargetType;
        private RaycastTargetType FindRaycastTargetType()
        {
            foreach (var component in GetComponents<Component>())
                switch (component)
                {
                    case Collider _: return RaycastTargetType.Collider;
                    case SpriteRenderer _: return RaycastTargetType.Sprite;
                    case TextMeshPro _: return RaycastTargetType.TextMeshPro;
                }
            return RaycastTargetType.Collider;
        }
        private protected void TryFitBoxColliderToMesh()
        {
            if (Trigger.TryAs(out BoxCollider boxCollider)
            && TryGet(out MeshFilter meshFilter))
                boxCollider.size = meshFilter.mesh.bounds.size;
        }
        private bool SpriteRaycastTest(Vector3 position)
        {
            if (Get<SpriteRenderer>().sprite.TryNonNull(out var sprite)
            && sprite.texture.isReadable)
                return sprite.PositionToPixelAlpha(position, transform) >= 0.5f;
            return true;
        }
        private void TMPTryFitBoxColliderOnTextChanged(UnityEngine.Object tmp)
        {
            if (tmp == Get<TextMeshPro>())
                TryFitBoxColliderToMesh();
        }

        // Defines
        private enum RaycastTargetType
        {
            Collider,
            Sprite,
            TextMeshPro,
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _raycastTests = new HashSet<Func<Vector3, bool>>();
            _raycastTargetType = FindRaycastTargetType();
            Trigger = Get<Collider>();
            TryFitBoxColliderToMesh();

            switch (_raycastTargetType)
            {
                case RaycastTargetType.Sprite: AddRaycastTest(SpriteRaycastTest); break;
                case RaycastTargetType.TextMeshPro: TMPro_EventManager.TEXT_CHANGED_EVENT.Add(TMPTryFitBoxColliderOnTextChanged); break;
            }
        }

        protected override void PlayDestroy()
        {
            base.PlayDestroy();
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(TMPTryFitBoxColliderOnTextChanged);
        }

#if CACHED_COMPONENTS
        protected override void DefineCachedComponents()
        {
            base.DefineCachedComponents();
            TryAddToCache<Collider>();
        }
#endif
    }
}


/*  
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



    if (TryGet(out Selectable selectable))
    {
        SubscribeAuto(OnGainHighlight, selectable.OnGainHighlight.Invoke);
        SubscribeAuto(OnLoseHighlight, selectable.OnLoseHighlight.Invoke);
        SubscribeAuto(OnPress, (button, position) => TryInvokeSelectableOnPress(selectable, button));
        SubscribeAuto(OnHold, (button, position) => TryInvokeSelectableOnHold(selectable, button));
        SubscribeAuto(OnRelease, (button, isClick) => TryInvokeSelectableOnRelease(selectable, button, isClick));
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

    private void AssignLayer()
    => gameObject.layer = LayerMask.NameToLayer(nameof(Cursorable));
    AssignLayer();
*/
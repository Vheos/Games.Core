namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Tools.Extensions.General;
    using Tools.Extensions.UnityObjects;
    using System.Linq;

    [RequireComponent(typeof(Collider))]
    [DisallowMultipleComponent]
    public class Raycastable : ABaseComponent
    {
        // Publics
        public Collider Collider
        { get; private set; }
        public void AddRaycastTest(Func<Vector3, bool> test)
        => _raycastTests.Add(test);
        public bool PerformRaycastTests(Vector3 position)
        {
            if (_raycastTests != null)
                foreach (Func<Vector3, bool> test in _raycastTests)
                    if (!test(position))
                        return false;
            return true;
        }
        public Component RaycastTarget
        {
            get => _raycastTarget;
            set
            {
                if (value == _raycastTarget)
                    return;
                else if (!IsValidRaycastTarget(value))
                {
                    Disable();
                    return;
                }

                switch (_raycastTarget)
                {
                    case SpriteRenderer: _raycastTests.Remove(SpriteRenderer_RaycastTest); break;
                    //case TextMeshPro: TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(TextMeshPro_OnTextChanged); break;
                }

                _raycastTarget = value;

                switch (_raycastTarget)
                {
                    case SpriteRenderer t:
                        _raycastTests.Add(SpriteRenderer_RaycastTest);
                        TryFitBoxColliderToRenderer(t);
                        break;
                    case TextMeshPro t:
                        //TMPro_EventManager.TEXT_CHANGED_EVENT.Add(TextMeshPro_OnTextChanged);
                        TryFitBoxColliderToRenderer(t.renderer);
                        break;
                }
            }
        }

        // Privates
        private Component _raycastTarget;
        private readonly HashSet<Func<Vector3, bool>> _raycastTests = new();
        private bool IsValidRaycastTarget(Component component)
        => component switch
        {
            UnityEngine.Collider or SpriteRenderer or TextMeshPro => true,
            _ => false,
        };
        private Component FindFirstValidRaycastTarget()
        => GetComponents<Component>().FirstOrDefault(t => IsValidRaycastTarget(t));
        private void TryFitBoxColliderToRenderer(Renderer renderer)
        {
            if (!Collider.TryAs(out BoxCollider boxCollider))
                return;

            boxCollider.size = renderer.localBounds.size;
        }
        private bool SpriteRenderer_RaycastTest(Vector3 position)
        {
            if (_raycastTarget.As<SpriteRenderer>().sprite.TryNonNull(out var sprite)
            && sprite.texture.isReadable)
                return sprite.PositionToPixelAlpha(position, transform) >= 0.5f;
            return true;
        }
        private void TextMeshPro_OnTextChanged(UnityEngine.Object obj)
        {
            if (obj != _raycastTarget)
                return;

            TryFitBoxColliderToRenderer(_raycastTarget.As<TextMeshPro>().renderer);
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayStart();
            Collider = Get<Collider>();
            RaycastTarget = FindFirstValidRaycastTarget();
        }
    }
}
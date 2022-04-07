namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Tools.Extensions.General;
    using Tools.Extensions.UnityObjects;

    [RequireComponent(typeof(Collider))]
    [DisallowMultipleComponent]
    public class Raycastable : ABaseComponent
    {
        // Publics
        public Collider RaycastCollider
        { get; private set; }
        public Component RaycastComponent
        {
            get => _raycastComponent;
            set
            {
                if (value == _raycastComponent)
                    return;

                if (!IsValidRaycastTarget(value))
                {
                    Disable();
                    return;
                }

                // Clean up after previous component
                switch (_raycastComponent)
                {
                    case SpriteRenderer:
                        _tests.Remove(SpriteRenderer_RaycastTest);
                        break;
                    case TextMeshPro:
                        break;
                }

                // Assign new component
                _raycastComponent = value;
                switch (_raycastComponent)
                {
                    case SpriteRenderer t:
                        RaycastRenderer = t;
                        _tests.Add(SpriteRenderer_RaycastTest);
                        TryFitBoxColliderToRenderer();
                        break;
                    case TextMeshPro t:
                        RaycastRenderer = t.renderer;
                        t.ForceMeshUpdate();
                        TryFitBoxColliderToRenderer();
                        break;
                    default:
                        RaycastRenderer = Get<Renderer>();
                        break;
                }
            }
        }
        public Renderer RaycastRenderer
        { get; private set; }
        public void AddTest(Func<Vector3, bool> test)
        => _tests.Add(test);
        public void RemoveTest(Func<Vector3, bool> test)
        => _tests.Remove(test);
        public bool Raycast(Ray ray, out RaycastHit hit)
        => RaycastCollider.Raycast(ray, out hit, float.PositiveInfinity)
        && CanHit(hit.point);

        // Privates
        private Component _raycastComponent;
        private readonly HashSet<Func<Vector3, bool>> _tests = new();
        private bool IsValidRaycastTarget(Component component)
        => component is UnityEngine.Collider or SpriteRenderer or TextMeshPro;
        private Component FindFirstValidRaycastTarget()
        => GetComponents<Component>().FirstOrDefault(t => IsValidRaycastTarget(t));
        private void TryFitBoxColliderToRenderer()
        {
            if (!RaycastCollider.TryAs(out BoxCollider boxCollider))
                return;

            var targetSize = RaycastRenderer.localBounds.size;
            if (RaycastRenderer is SpriteRenderer)
                targetSize.z = 0f;

            boxCollider.size = targetSize;
        }
        private bool SpriteRenderer_RaycastTest(Vector3 position)
        {
            if (_raycastComponent.As<SpriteRenderer>().sprite.TryNonNull(out var sprite)
            && sprite.texture.isReadable)
                return sprite.PositionToPixelAlpha(position, transform) >= 0.5f;
            return true;
        }
        private bool CanHit(Vector3 position)
        {
            foreach (Func<Vector3, bool> test in _tests)
                if (!test(position))
                    return false;
            return true;
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            RaycastCollider = Get<Collider>();
            RaycastComponent = FindFirstValidRaycastTarget();
        }
    }
}

/*
                        //case TextMeshPro: TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(TextMeshPro_OnTextChanged); break;
                           //TMPro_EventManager.TEXT_CHANGED_EVENT.Add(TextMeshPro_OnTextChanged);
         private void TextMeshPro_OnTextChanged(UnityEngine.Object obj)
        {
            if (obj != _raycastTarget)
                return;

            TryFitBoxColliderToRenderer(_raycastTarget.As<TextMeshPro>().renderer);
        }
 */
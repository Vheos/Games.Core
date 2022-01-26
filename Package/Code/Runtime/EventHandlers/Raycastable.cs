namespace Vheos.Games.Core
{
    using System;
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
        private void TryFitBoxColliderToMesh()
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
        private void TMPTryFitBoxColliderOnTextChanged(UnityEngine.Object textMeshPro)
        {
            if (textMeshPro == Get<TextMeshPro>())
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
    }
}
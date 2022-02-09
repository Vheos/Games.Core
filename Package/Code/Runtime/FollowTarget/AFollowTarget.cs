namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.Math;
    using Vheos.Tools.Extensions.General;
    using Vheos.Tools.Utilities;

    [RequireComponent(typeof(Updatable))]
    abstract public class AFollowTarget : ABaseComponent
    {
        // Inspector
        [field: SerializeField] public Transform Transform { get; private set; } = null;
        [field: SerializeField] public Vector3 Vector { get; private set; } = Vector3.zero;
        [field: SerializeField] public Vector3 Offset { get; private set; } = Vector3.zero;
        [field: SerializeField] public Axes LockedAxes { get; private set; } = 0;
        [field: SerializeField, Range(0f, 1f)] public float HalfTime { get; private set; } = 0.25f;

        // Publics
        public void SetTarget(GameObject target, bool followInstantly = false)
        {
            if (target.transform != this.transform)
            {
                Transform = target.transform;
                _targetType = TargetType.Transform;
                enabled = Transform != null;
            }

            if (followInstantly)
                FollowOnUpdate(1f);
        }
        public void SetTarget(Component target, bool followInstantly = false)
        => SetTarget(target.gameObject, followInstantly);
        public void SetTarget(Vector3 position, bool followInstantly = false)
        {
            Vector = position;
            _targetType = TargetType.Vector;
            enabled = true;

            if (followInstantly)
                FollowOnUpdate(1f);
        }

        // Private
        protected TargetType _targetType;
        private void TryFollowTargetOnUpdate()
        {
            if (_targetType == TargetType.Transform
            && Transform == null)
                return;

            FollowOnUpdate(Utility.HalfTimeToLerpAlpha(HalfTime));
        }
        abstract protected void FollowOnUpdate(float lerpAlpha);
        abstract protected Vector3 TargetVector
        { get; }

        // Defines
        protected enum TargetType
        {
            Transform,
            Vector,
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _targetType = Transform != null ? TargetType.Transform : TargetType.Vector;
            Get<Updatable>().OnUpdate.SubEnableDisable(this, TryFollowTargetOnUpdate);
        }
    }
}
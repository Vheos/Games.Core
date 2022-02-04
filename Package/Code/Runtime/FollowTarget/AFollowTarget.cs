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
        [SerializeField] protected Transform _Transform = null;
        [SerializeField] protected Vector3 _Vector = Vector3.zero;
        [SerializeField] protected Vector3 _Offset = Vector3.zero;
        [SerializeField] protected Axes _LockedAxes = 0;
        [SerializeField] [Range(0f, 1f)] protected float _HalfTime = 0.25f;

        // Publics
        public void SetTarget(GameObject target, bool followInstantly = false)
        {
            if (target.transform != this.transform)
            {
                _Transform = target.transform;
                _targetType = TargetType.Transform;
                enabled = _Transform != null;
            }

            if (followInstantly)
                FollowOnUpdate(1f);
        }
        public void SetTarget(Component target, bool followInstantly = false)
        => SetTarget(target.gameObject, followInstantly);
        public void SetTarget(Vector3 position, bool followInstantly = false)
        {
            _Vector = position;
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
            && _Transform == null)
                return;

            FollowOnUpdate(Utility.HalfTimeToLerpAlpha(_HalfTime));
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
            _targetType = _Transform != null ? TargetType.Transform : TargetType.Vector;
            Get<Updatable>().OnUpdate.SubEnableDisable(this, TryFollowTargetOnUpdate);
        }
    }
}
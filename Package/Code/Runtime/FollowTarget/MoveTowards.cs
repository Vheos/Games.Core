namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.Math;

    public class MoveTowards : AFollowTarget
    {
        // Publics
        public Vector3 FinalPosition
        {
            get
            {
                Vector3 targetPosition = TargetVector;
                if (LockedAxes != 0)
                {
                    Vector3 currentPosition = transform.position;
                    if (LockedAxes.HasFlag(Axes.X))
                        targetPosition.x = currentPosition.x;
                    if (LockedAxes.HasFlag(Axes.Y))
                        targetPosition.y = currentPosition.y;
                    if (LockedAxes.HasFlag(Axes.Z))
                        targetPosition.z = currentPosition.z;
                }
                return targetPosition + Offset;
            }
        }

        // Overrides
        protected override Vector3 TargetVector
        => _targetType switch
        {
            TargetType.Transform => Transform.position,
            TargetType.Vector => Vector,
            _ => default,
        };
        protected override void FollowOnUpdate(float lerpAlpha)
        => transform.position = transform.position.Lerp(FinalPosition, lerpAlpha);
    }
}
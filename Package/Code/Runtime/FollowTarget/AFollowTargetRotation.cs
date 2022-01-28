namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.Math;

    abstract public class AFollowTargetRotation : AFollowTarget
    {
        // Publics
        public Quaternion FinalRotation
        {
            get
            {
                Vector3 targetAngles = TargetVector;
                if (_LockedAxes != 0)
                {
                    Vector3 currentAngles = transform.rotation.eulerAngles;
                    if (_LockedAxes.HasFlag(Axes.X))
                        targetAngles.x = currentAngles.x;
                    if (_LockedAxes.HasFlag(Axes.Y))
                        targetAngles.y = currentAngles.y;
                    if (_LockedAxes.HasFlag(Axes.Z))
                        targetAngles.z = currentAngles.z;
                }
                return Quaternion.Euler(targetAngles + _Offset);
            }
        }

        // Overrides
        protected override void FollowOnUpdate(float lerpAlpha)
        => transform.rotation = transform.rotation.Lerp(FinalRotation, lerpAlpha);
    }
}
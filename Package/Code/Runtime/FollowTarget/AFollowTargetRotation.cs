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
                if (LockedAxes != 0)
                {
                    Vector3 currentAngles = transform.rotation.eulerAngles;
                    if (LockedAxes.HasFlag(Axes.X))
                        targetAngles.x = currentAngles.x;
                    if (LockedAxes.HasFlag(Axes.Y))
                        targetAngles.y = currentAngles.y;
                    if (LockedAxes.HasFlag(Axes.Z))
                        targetAngles.z = currentAngles.z;
                }
                return Quaternion.Euler(targetAngles + Offset);
            }
        }

        // Overrides
        protected override void FollowOnUpdate(float lerpAlpha)
        => transform.rotation = transform.rotation.Lerp(FinalRotation, lerpAlpha);
    }
}
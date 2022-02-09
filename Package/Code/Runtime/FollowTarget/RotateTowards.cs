namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.Math;
    using Tools.Extensions.UnityObjects;

    public class RotateTowards : AFollowTargetRotation
    {
        // Overrides
        protected override Vector3 TargetVector
        => Quaternion.LookRotation(_targetType switch
        {
            TargetType.Transform => this.DirectionTowards(Transform),
            TargetType.Vector => this.DirectionTowards(Vector),
            _ => default,
        })
        .eulerAngles;
    }
}
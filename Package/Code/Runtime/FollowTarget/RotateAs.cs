namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    public class RotateAs : AFollowTargetRotation
    {
        // Overrides
        protected override Vector3 TargetVector
        => _targetType switch
        {
            TargetType.Transform => Transform.rotation.eulerAngles,
            TargetType.Vector => Vector,
            _ => default,
        };
    }
}
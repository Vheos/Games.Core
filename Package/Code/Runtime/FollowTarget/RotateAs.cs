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
            TargetType.Transform => _Transform.rotation.eulerAngles,
            TargetType.Vector => _Vector,
            _ => default,
        };
    }
}
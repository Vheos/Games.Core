namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    using TMPro;
    using Tools.Extensions.Math;

    static public class Tweener_Extensions
    {
        // General
        static public Tween Animate(this Component t, float duration)
        => Tweener.Animate(duration).SetGUID(t);
        static public Tween Animate(this Component t, float duration, ConflictResolution conflictResolution)
        => Tweener.Animate(duration).SetGUID(t).SetConflictResolution(conflictResolution);
        static public void StopAnimations(this Component t)
        => Tweener.Stop(t);

        // Transform        
        static public Tween Position(this Tween t, Transform c, Vector3 to)
        => t.AddModifier(v => c.position += v, to - c.position);
        static public Tween LocalPosition(this Tween t, Transform c, Vector3 to)
        => t.AddModifier(v => c.localPosition += v, to - c.localPosition);
        static public Tween Rotation(this Tween t, Transform c, Quaternion to)
        => t.AddModifier(v => c.rotation *= v, to.Sub(c.rotation));
        static public Tween LocalRotation(this Tween t, Transform c, Quaternion to)
        => t.AddModifier(v => c.localRotation *= v, to.Sub(c.localRotation));
        static public Tween LocalScale(this Tween t, Transform c, Vector3 to)
        => t.AddModifier(v => c.localScale += v, to - c.localScale);
        static public Tween LocalScaleRatio(this Tween t, Transform c, Vector3 ratio)
        => t.AddModifier(v => c.localScale = c.localScale.Mul(v), ratio, AssignmentType.Multiplicative);
        static public Tween LocalScaleRatio(this Tween t, Transform c, float ratio)
        => t.AddModifier(v => c.localScale *= v, ratio, AssignmentType.Multiplicative);

        // SpriteRenderer
        static public Tween Color(this Tween t, SpriteRenderer c, Color to)
        => t.AddModifier(v => c.color += v, to - c.color);
        static public Tween ColorRatio(this Tween t, SpriteRenderer c, Color ratio)
        => t.AddModifier(v => c.color *= v, ratio, AssignmentType.Multiplicative);
        static public Tween ColorRatio(this Tween t, SpriteRenderer c, float ratio)
        => t.AddModifier(v => c.color *= v, ratio, AssignmentType.Multiplicative);
        static public Tween Alpha(this Tween t, SpriteRenderer c, float to)
        => t.AddModifier(v => c.color += new Color(0f, 0f, 0f, v), to - c.color.a);
        static public Tween AlphaRatio(this Tween t, SpriteRenderer c, float ratio)
        => t.AddModifier(v => c.color *= new Color(1f, 1f, 1f, v), ratio);

        // TextMeshPro
        static public Tween Color(this Tween t, TextMeshPro c, Color to)
        => t.AddModifier(v => c.color += v, to - c.color);
        static public Tween ColorRatio(this Tween t, TextMeshPro c, Color ratio)
        => t.AddModifier(v => c.color *= v, ratio, AssignmentType.Multiplicative);
        static public Tween ColorRatio(this Tween t, TextMeshPro c, float ratio)
        => t.AddModifier(v => c.color *= v, ratio, AssignmentType.Multiplicative);
        static public Tween Alpha(this Tween t, TextMeshPro c, float to)
        => t.AddModifier(v => c.alpha += v, to - c.color.a);
        static public Tween AlphaRatio(this Tween t, TextMeshPro c, float ratio)
        => t.AddModifier(v => c.alpha *= v, ratio);
    }
}
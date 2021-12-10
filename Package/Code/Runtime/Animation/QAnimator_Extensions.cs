namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    using TMPro;
    using Tools.Extensions.Math;

    static public class QAnimator_Extensions
    {
        // General
        static public QAnimation Animate(this Component t, float duration)
        => QAnimator.Animate(duration).Set(t);
        static public QAnimation Animate(this Component t, float duration, ConflictResolution conflictResolution)
        => QAnimator.Animate(duration).Set(t).Set(conflictResolution);
        static public void StopAnimations(this Component t)
        => QAnimator.Stop(t);

        // Transform        
        static public QAnimation Position(this QAnimation t, Transform c, Vector3 to)
        => t.Custom(v => c.position += v, to - c.position);
        static public QAnimation LocalPosition(this QAnimation t, Transform c, Vector3 to)
        => t.Custom(v => c.localPosition += v, to - c.localPosition);
        static public QAnimation Rotation(this QAnimation t, Transform c, Quaternion to)
        => t.Custom(v => c.rotation *= v, to.Sub(c.rotation));
        static public QAnimation LocalRotation(this QAnimation t, Transform c, Quaternion to)
        => t.Custom(v => c.localRotation *= v, to.Sub(c.localRotation));
        static public QAnimation LocalScale(this QAnimation t, Transform c, Vector3 to)
        => t.Custom(v => c.localScale += v, to - c.localScale);
        static public QAnimation LocalScaleRatio(this QAnimation t, Transform c, Vector3 ratio)
        => t.Custom(v => c.localScale = c.localScale.Mul(v), ratio, AssignmentType.Multiplicative);
        static public QAnimation LocalScaleRatio(this QAnimation t, Transform c, float ratio)
        => t.Custom(v => c.localScale *= v, ratio, AssignmentType.Multiplicative);

        // SpriteRenderer
        static public QAnimation Color(this QAnimation t, SpriteRenderer c, Color to)
        => t.Custom(v => c.color += v, to - c.color);
        static public QAnimation ColorRatio(this QAnimation t, SpriteRenderer c, Color ratio)
        => t.Custom(v => c.color *= v, ratio, AssignmentType.Multiplicative);
        static public QAnimation ColorRatio(this QAnimation t, SpriteRenderer c, float ratio)
        => t.Custom(v => c.color *= v, ratio, AssignmentType.Multiplicative);
        static public QAnimation Alpha(this QAnimation t, SpriteRenderer c, float to)
        => t.Custom(v => c.color += new Color(0f, 0f, 0f, v), to - c.color.a);
        static public QAnimation AlphaRatio(this QAnimation t, SpriteRenderer c, float ratio)
        => t.Custom(v => c.color *= new Color(1f, 1f, 1f, v), ratio);

        // TextMeshPro
        static public QAnimation Color(this QAnimation t, TextMeshPro c, Color to)
        => t.Custom(v => c.color += v, to - c.color);
        static public QAnimation ColorRatio(this QAnimation t, TextMeshPro c, Color ratio)
        => t.Custom(v => c.color *= v, ratio, AssignmentType.Multiplicative);
        static public QAnimation ColorRatio(this QAnimation t, TextMeshPro c, float ratio)
        => t.Custom(v => c.color *= v, ratio, AssignmentType.Multiplicative);
        static public QAnimation Alpha(this QAnimation t, TextMeshPro c, float to)
        => t.Custom(v => c.alpha += v, to - c.color.a);
        static public QAnimation AlphaRatio(this QAnimation t, TextMeshPro c, float ratio)
        => t.Custom(v => c.alpha *= v, ratio);
    }
}
namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    using TMPro;
    using Tools.Extensions.Math;
    using static QAnimator;

    static public class QAnimator_Extensions
    {
        // General
        static public QAnimation Animate(this Component t, float duration, ConflictResolution conflictResolution)
        => New(duration).Set(conflictResolution).Set(t);

        // Transform        
        static public QAnimation AnimatePosition(this Component t, float duration, Vector3 to)
        => New(duration).Add(v => t.transform.position += v, to - t.transform.position);
        static public QAnimation AnimateLocalPosition(this Component t, float duration, Vector3 to)
        => New(duration).Add(v => t.transform.localPosition += v, to - t.transform.localPosition);
        static public QAnimation AnimateRotation(this Component t, float duration, Quaternion to)
        => New(duration).Add(v => t.transform.rotation *= v, to.Sub(t.transform.rotation));
        static public QAnimation AnimateLocalRotation(this Component t, float duration, Quaternion to)
        => New(duration).Add(v => t.transform.localRotation *= v, to.Sub(t.transform.localRotation));
        static public QAnimation AnimateLocalScale(this Component t, float duration, Vector3 to)
        => New(duration).Add(v => t.transform.localScale += v, to - t.transform.localScale);
        static public QAnimation AnimateLocalScaleRatio(this Component t, float duration, Vector3 ratio)
        => New(duration).Add(v => t.transform.localScale = t.transform.localScale.Mul(v), ratio, AssignmentType.Multiplicative);
        static public QAnimation AnimateLocalScaleRatio(this Component t, float duration, float ratio)
        => New(duration).Add(v => t.transform.localScale *= v, ratio, AssignmentType.Multiplicative);
        static public QAnimation AnimatePosition(this Component t, float duration, Vector3 to, ConflictResolution conflictResolution)
        => t.AnimatePosition(duration, to).Set(conflictResolution).Set(t);
        static public QAnimation AnimateLocalPosition(this Component t, float duration, Vector3 to, ConflictResolution conflictResolution)
        => t.AnimateLocalPosition(duration, to).Set(conflictResolution).Set(t);
        static public QAnimation AnimateRotation(this Component t, float duration, Quaternion to, ConflictResolution conflictResolution)
        => t.AnimateRotation(duration, to).Set(conflictResolution).Set(t);
        static public QAnimation AnimateLocalRotation(this Component t, float duration, Quaternion to, ConflictResolution conflictResolution)
        => t.AnimateLocalRotation(duration, to).Set(conflictResolution).Set(t);
        static public QAnimation AnimateLocalScale(this Component t, float duration, Vector3 to, ConflictResolution conflictResolution)
        => t.AnimateLocalScale(duration, to).Set(conflictResolution).Set(t);
        static public QAnimation AnimateLocalScaleRatio(this Component t, float duration, Vector3 ratio, ConflictResolution conflictResolution)
        => t.AnimateLocalScaleRatio(duration, ratio).Set(conflictResolution).Set(t);
        static public QAnimation AnimateLocalScaleRatio(this Component t, float duration, float ratio, ConflictResolution conflictResolution)
        => t.AnimateLocalScaleRatio(duration, ratio).Set(conflictResolution).Set(t);

        // SpriteRenderer
        static public QAnimation AnimateColor(this SpriteRenderer t, float duration, Color to)
        => New(duration).Add(v => t.color += v, to - t.color);
        static public QAnimation AnimateColorRatio(this SpriteRenderer t, float duration, Color ratio)
        => New(duration).Add(v => t.color *= v, ratio, AssignmentType.Multiplicative);
        static public QAnimation AnimateColorRatio(this SpriteRenderer t, float duration, float ratio)
        => New(duration).Add(v => t.color *= v, ratio, AssignmentType.Multiplicative);
        static public QAnimation AnimateAlpha(this SpriteRenderer t, float duration, float to)
        => New(duration).Add(v => t.color += new Color(0f, 0f, 0f, v), to - t.color.a);
        static public QAnimation AnimateAlphaRatio(this SpriteRenderer t, float duration, float ratio)
        => New(duration).Add(v => t.color *= new Color(1f, 1f, 1f, v), ratio);
        static public QAnimation AnimateColor(this SpriteRenderer t, float duration, Color to, ConflictResolution conflictResolution)
        => t.AnimateColor(duration, to).Set(conflictResolution).Set(t);
        static public QAnimation AnimateColorRatio(this SpriteRenderer t, float duration, Color ratio, ConflictResolution conflictResolution)
        => t.AnimateColorRatio(duration, ratio).Set(conflictResolution).Set(t);
        static public QAnimation AnimateColorRatio(this SpriteRenderer t, float duration, float ratio, ConflictResolution conflictResolution)
        => t.AnimateColorRatio(duration, ratio).Set(conflictResolution).Set(t);
        static public QAnimation AnimateAlpha(this SpriteRenderer t, float duration, float to, ConflictResolution conflictResolution)
        => t.AnimateAlpha(duration, to).Set(conflictResolution).Set(t);
        static public QAnimation AnimateAlphaRatio(this SpriteRenderer t, float duration, float ratio, ConflictResolution conflictResolution)
        => t.AnimateAlphaRatio(duration, ratio).Set(conflictResolution).Set(t);

        // TextMeshPro
        static public QAnimation AnimateColor(this TextMeshPro t, float duration, Color to)
        => New(duration).Add(v => t.color += v, to - t.color);
        static public QAnimation AnimateColorRatio(this TextMeshPro t, float duration, Color ratio)
        => New(duration).Add(v => t.color *= v, ratio, AssignmentType.Multiplicative);
        static public QAnimation AnimateColorRatio(this TextMeshPro t, float duration, float ratio)
        => New(duration).Add(v => t.color *= v, ratio, AssignmentType.Multiplicative);
        static public QAnimation AnimateAlpha(this TextMeshPro t, float duration, float to)
        => New(duration).Add(v => t.alpha += v, to - t.color.a);
        static public QAnimation AnimateAlphaRatio(this TextMeshPro t, float duration, float ratio)
        => New(duration).Add(v => t.alpha *= v, ratio);
        static public QAnimation AnimateColor(this TextMeshPro t, float duration, Color to, ConflictResolution conflictResolution)
        => t.AnimateColor(duration, to).Set(conflictResolution).Set(t);
        static public QAnimation AnimateColorRatio(this TextMeshPro t, float duration, Color ratio, ConflictResolution conflictResolution)
        => t.AnimateColorRatio(duration, ratio).Set(conflictResolution).Set(t);
        static public QAnimation AnimateColorRatio(this TextMeshPro t, float duration, float ratio, ConflictResolution conflictResolution)
        => t.AnimateColorRatio(duration, ratio).Set(conflictResolution).Set(t);
        static public QAnimation AnimateAlpha(this TextMeshPro t, float duration, float to, ConflictResolution conflictResolution)
        => t.AnimateAlpha(duration, to).Set(conflictResolution).Set(t);
        static public QAnimation AnimateAlphaRatio(this TextMeshPro t, float duration, float ratio, ConflictResolution conflictResolution)
        => t.AnimateAlphaRatio(duration, ratio).Set(conflictResolution).Set(t);
    }
}
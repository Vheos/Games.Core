namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.Math;
    using static QAnimator;

    static public class QAnimator_Extensions
    {
        // Transform
        static public void AnimatePosition(this Component t, Vector3 to, float duration)
        => Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration)
        => Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration);
        static public void AnimateRotation(this Component t, Quaternion to, float duration)
        => Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration)
        => Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration)
        => Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration)
        => Animate(t.AssignLocalScaleRatioFunc, ratio, duration, OptionalsMultiplicative);
        static public void AnimateLocalScaleRatio(this Component t, float ratio, float duration)
        => Animate(t.AssignLocalScaleRatioFunc, ratio, duration, OptionalsMultiplicative);
        static public void AnimatePosition(this Component t, Vector3 to, float duration, OptionalParameters optionals)
        => Animate(t.AssignPositionFunc, t.PositionOffsetTo(to), duration, optionals);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, OptionalParameters optionals)
        => Animate(t.AssignLocalPositionFunc, t.LocalPositionOffsetTo(to), duration, optionals);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, OptionalParameters optionals)
        => Animate(t.AssignRotationFunc, t.RotationOffsetTo(to), duration, optionals);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, OptionalParameters optionals)
        => Animate(t.AssignLocalRotationFunc, t.LocalRotationOffsetTo(to), duration, optionals);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, OptionalParameters optionals)
        => Animate(t.AssignLocalScaleFunc, t.LocalScaleOffsetTo(to), duration, optionals);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, OptionalParameters optionals)
        => Animate(t.AssignLocalScaleRatioFunc, ratio, duration, optionals.Set(AssignmentType.Multiplicative));
        static public void AnimateLocalScaleRatio(this Component t, float ratio, float duration, OptionalParameters optionals)
        => Animate(t.AssignLocalScaleRatioFunc, ratio, duration, optionals.Set(AssignmentType.Multiplicative));

        // SpriteRenderer
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration)
        => Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, OptionalsMultiplicative);
        static public void AnimateColorRatio(this SpriteRenderer t, Vector4 ratio, float duration)
        => Animate(t.AssignColorRatioFunc, ratio, duration, OptionalsMultiplicative);
        static public void AnimateColorRatio(this SpriteRenderer t, float ratio, float duration)
        => Animate(t.AssignColorRatioFunc, ratio, duration, OptionalsMultiplicative);
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, OptionalParameters optionals)
        => Animate(t.AssignColorFunc, t.ColorOffsetTo(to), duration, optionals);
        static public void AnimateColorRatio(this SpriteRenderer t, Vector4 ratio, float duration, OptionalParameters optionals)
        => Animate(t.AssignColorRatioFunc, ratio, duration, optionals.Set(AssignmentType.Multiplicative));
        static public void AnimateColorRatio(this SpriteRenderer t, float ratio, float duration, OptionalParameters optionals)
        => Animate(t.AssignColorRatioFunc, ratio, duration, optionals.Set(AssignmentType.Multiplicative));

        #region Privates
        // Assing
        static private void AssignPositionFunc(this Component t, Vector3 a)
        => t.transform.position += a;
        static private void AssignLocalPositionFunc(this Component t, Vector3 a)
        => t.transform.localPosition += a;
        static private void AssignRotationFunc(this Component t, Quaternion a)
        => t.transform.rotation = t.transform.rotation.Add(a);
        static private void AssignLocalRotationFunc(this Component t, Quaternion a)
        => t.transform.localRotation = t.transform.localRotation.Add(a);
        static private void AssignLocalScaleFunc(this Component t, Vector3 a)
        => t.transform.localScale += a;
        static private void AssignColorFunc(this SpriteRenderer t, Color a)
        => t.color += a;

        // Offset
        static private Vector3 PositionOffsetTo(this Component t, Vector3 a)
        => a - t.transform.position;
        static private Vector3 LocalPositionOffsetTo(this Component t, Vector3 a)
        => a - t.transform.localPosition;
        static private Quaternion RotationOffsetTo(this Component t, Quaternion a)
        => a.Sub(t.transform.rotation);
        static private Quaternion LocalRotationOffsetTo(this Component t, Quaternion a)
        => a.Sub(t.transform.localRotation);
        static private Vector3 LocalScaleOffsetTo(this Component t, Vector3 a)
        => a - t.transform.localScale;
        static private Color ColorOffsetTo(this SpriteRenderer t, Color a)
        => a - t.color;

        // Ratio
        static private void AssignLocalScaleRatioFunc(this Component t, Vector3 a)
        => t.transform.localScale = t.transform.localScale.Mul(a);
        static private void AssignLocalScaleRatioFunc(this Component t, float a)
        => t.transform.localScale *= a;
        static private void AssignColorRatioFunc(this SpriteRenderer t, Vector4 a)
        => t.color *= a;
        static private void AssignColorRatioFunc(this SpriteRenderer t, float a)
        => t.color *= a;
        #endregion
    }
}
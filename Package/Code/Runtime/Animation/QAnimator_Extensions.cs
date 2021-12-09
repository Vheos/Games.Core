namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    using TMPro;
    using Tools.Extensions.Collections;
    using Tools.Extensions.Math;
    using static QAnimator;

    static public class QAnimator_Extensions
    {
        #region Transform
        // Basic
        static public void AnimatePosition(this Component t, Vector3 to, float duration)
        => Animate(t.SetAddPosition, t.PositionOffsetTo(to), duration);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration)
        => Animate(t.SetAddLocalPosition, t.LocalPositionOffsetTo(to), duration);
        static public void AnimateRotation(this Component t, Quaternion to, float duration)
        => Animate(t.SetAddRotation, t.RotationOffsetTo(to), duration);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration)
        => Animate(t.SetAddLocalRotation, t.LocalRotationOffsetTo(to), duration);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration)
        => Animate(t.SetAddLocalScale, t.LocalScaleOffsetTo(to), duration);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration)
        => Animate(t.SetMulLocalScale, ratio, duration, AssignmentType.Multiplicative);
        static public void AnimateLocalScaleRatio(this Component t, float ratio, float duration)
        => Animate(t.SetMUlLocalScale, ratio, duration, AssignmentType.Multiplicative);

        // Optionals
        static public void AnimatePosition(this Component t, Vector3 to, float duration, OptionalParameters optionals)
        => Animate(t.SetAddPosition, t.PositionOffsetTo(to), duration, optionals);
        static public void AnimateLocalPosition(this Component t, Vector3 to, float duration, OptionalParameters optionals)
        => Animate(t.SetAddLocalPosition, t.LocalPositionOffsetTo(to), duration, optionals);
        static public void AnimateRotation(this Component t, Quaternion to, float duration, OptionalParameters optionals)
        => Animate(t.SetAddRotation, t.RotationOffsetTo(to), duration, optionals);
        static public void AnimateLocalRotation(this Component t, Quaternion to, float duration, OptionalParameters optionals)
        => Animate(t.SetAddLocalRotation, t.LocalRotationOffsetTo(to), duration, optionals);
        static public void AnimateLocalScale(this Component t, Vector3 to, float duration, OptionalParameters optionals)
        => Animate(t.SetAddLocalScale, t.LocalScaleOffsetTo(to), duration, optionals);
        static public void AnimateLocalScaleRatio(this Component t, Vector3 ratio, float duration, OptionalParameters optionals)
        => Animate(t.SetMulLocalScale, ratio, duration, optionals.New(AssignmentType.Multiplicative));
        static public void AnimateLocalScaleRatio(this Component t, float ratio, float duration, OptionalParameters optionals)
        => Animate(t.SetMUlLocalScale, ratio, duration, optionals.New(AssignmentType.Multiplicative));

        // Group
        static public void GroupAnimatePosition(this Component t, Vector3 to)
        => GroupAnimate(t.SetAddPosition, t.PositionOffsetTo(to));
        static public void GroupAnimateLocalPosition(this Component t, Vector3 to)
        => GroupAnimate(t.SetAddLocalPosition, t.LocalPositionOffsetTo(to));
        static public void GroupAnimateRotation(this Component t, Quaternion to)
        => GroupAnimate(t.SetAddRotation, t.RotationOffsetTo(to));
        static public void GroupAnimateLocalRotation(this Component t, Quaternion to)
        => GroupAnimate(t.SetAddLocalRotation, t.LocalRotationOffsetTo(to));
        static public void GroupAnimateLocalScale(this Component t, Vector3 to)
        => GroupAnimate(t.SetAddLocalScale, t.LocalScaleOffsetTo(to));
        static public void GroupAnimateLocalScaleRatio(this Component t, Vector3 ratio)
        => GroupAnimate(t.SetMulLocalScale, ratio, AssignmentType.Multiplicative);
        static public void GroupAnimateLocalScaleRatio(this Component t, float ratio)
        => GroupAnimate(t.SetMUlLocalScale, ratio, AssignmentType.Multiplicative);

        // Privates
        static private void SetAddPosition(this Component t, Vector3 a)
        => t.transform.position += a;
        static private void SetAddLocalPosition(this Component t, Vector3 a)
        => t.transform.localPosition += a;
        static private void SetAddRotation(this Component t, Quaternion a)
        => t.transform.rotation *= a;
        static private void SetAddLocalRotation(this Component t, Quaternion a)
        => t.transform.localRotation *= a;
        static private void SetAddLocalScale(this Component t, Vector3 a)
        => t.transform.localScale += a;
        static private void SetMulLocalScale(this Component t, Vector3 a)
        => t.transform.localScale = t.transform.localScale.Mul(a);
        static private void SetMUlLocalScale(this Component t, float a)
        => t.transform.localScale *= a;
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
        #endregion

        #region SpriteRenderer
        // Basic
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration)
        => Animate(t.SetAddColor, t.ColorOffsetTo(to), duration);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration)
        => Animate(t.SetMulColor, ratio, duration, AssignmentType.Multiplicative);
        static public void AnimateColorRatio(this SpriteRenderer t, float ratio, float duration)
        => Animate(t.SetMulColor, ratio, duration, AssignmentType.Multiplicative);
        static public void AnimateAlpha(this SpriteRenderer t, float to, float duration)
        => Animate(t.SetAddAlpha, to - t.color.a, duration);
        static public void AnimateAlphaRatio(this SpriteRenderer t, float ratio, float duration)
        => Animate(t.SetMulAlpha, ratio, duration);

        // Optionals
        static public void AnimateColor(this SpriteRenderer t, Color to, float duration, OptionalParameters optionals)
        => Animate(t.SetAddColor, t.ColorOffsetTo(to), duration, optionals);
        static public void AnimateColorRatio(this SpriteRenderer t, Color ratio, float duration, OptionalParameters optionals)
        => Animate(t.SetMulColor, ratio, duration, optionals.New(AssignmentType.Multiplicative));
        static public void AnimateColorRatio(this SpriteRenderer t, float ratio, float duration, OptionalParameters optionals)
        => Animate(t.SetMulColor, ratio, duration, optionals.New(AssignmentType.Multiplicative));
        static public void AnimateAlpha(this SpriteRenderer t, float to, float duration, OptionalParameters optionals)
        => Animate(t.SetAddAlpha, to - t.color.a, duration, optionals);
        static public void AnimateAlphaRatio(this SpriteRenderer t, float ratio, float duration, OptionalParameters optionals)
        => Animate(t.SetMulAlpha, ratio, duration, optionals);

        // Group
        static public void GroupAnimateColor(this SpriteRenderer t, Color to)
        => GroupAnimate(t.SetAddColor, t.ColorOffsetTo(to));
        static public void GroupAnimateAlpha(this SpriteRenderer t, float to)
        => GroupAnimate(t.SetAddAlpha, t.AlphaOffsetTo(to));
        static public void GroupAnimateColorRatio(this SpriteRenderer t, Color ratio)
        => GroupAnimate(t.SetMulColor, ratio, AssignmentType.Multiplicative);
        static public void GroupAnimateColorRatio(this SpriteRenderer t, float ratio)
        => GroupAnimate(t.SetMulColor, ratio, AssignmentType.Multiplicative);
        static public void GroupAnimateAlphaRatio(this SpriteRenderer t, float ratio)
        => GroupAnimate(t.SetMulAlpha, ratio, AssignmentType.Multiplicative);

        // Privates
        static private void SetAddColor(this SpriteRenderer t, Color a)
        => t.color += a;
        static private void SetMulColor(this SpriteRenderer t, Color a)
        => t.color *= a;
        static private void SetMulColor(this SpriteRenderer t, float a)
        => t.color *= a;
        static private void SetAddAlpha(this SpriteRenderer t, float a)
        => t.color += new Color(0f, 0f, 0f, a);
        static private void SetMulAlpha(this SpriteRenderer t, float a)
        => t.color *= new Color(1f, 1f, 1f, a);
        static private Color ColorOffsetTo(this SpriteRenderer t, Color a)
        => a - t.color;
        static private float AlphaOffsetTo(this SpriteRenderer t, float a)
        => a - t.color.a;
        #endregion

        #region TextMeshPro
        // Basic
        static public void AnimateColor(this TextMeshPro t, Color to, float duration)
        => Animate(t.SetAddColor, t.ColorOffsetTo(to), duration);
        static public void AnimateColorRatio(this TextMeshPro t, Color ratio, float duration)
        => Animate(t.SetMulColor, ratio, duration, AssignmentType.Multiplicative);
        static public void AnimateColorRatio(this TextMeshPro t, float ratio, float duration)
        => Animate(t.SetMulColor, ratio, duration, AssignmentType.Multiplicative);
        static public void AnimateAlpha(this TextMeshPro t, float to, float duration)
        => Animate(t.SetAddAlpha, to - t.color.a, duration);
        static public void AnimateAlphaRatio(this TextMeshPro t, float ratio, float duration)
        => Animate(t.SetMulAlpha, ratio, duration);

        // Optionals
        static public void AnimateColor(this TextMeshPro t, Color to, float duration, OptionalParameters optionals)
        => Animate(t.SetAddColor, t.ColorOffsetTo(to), duration, optionals);
        static public void AnimateColorRatio(this TextMeshPro t, Color ratio, float duration, OptionalParameters optionals)
        => Animate(t.SetMulColor, ratio, duration, optionals.New(AssignmentType.Multiplicative));
        static public void AnimateColorRatio(this TextMeshPro t, float ratio, float duration, OptionalParameters optionals)
        => Animate(t.SetMulColor, ratio, duration, optionals.New(AssignmentType.Multiplicative));
        static public void AnimateAlpha(this TextMeshPro t, float to, float duration, OptionalParameters optionals)
        => Animate(t.SetAddAlpha, to - t.color.a, duration, optionals);
        static public void AnimateAlphaRatio(this TextMeshPro t, float ratio, float duration, OptionalParameters optionals)
        => Animate(t.SetMulAlpha, ratio, duration, optionals);

        // Group
        static public void GroupAnimateColor(this TextMeshPro t, Color to)
        => GroupAnimate(t.SetAddColor, t.ColorOffsetTo(to));
        static public void GroupAnimateAlpha(this TextMeshPro t, float to)
        => GroupAnimate(t.SetAddAlpha, t.AlphaOffsetTo(to));
        static public void GroupAnimateColorRatio(this TextMeshPro t, Color ratio)
        => GroupAnimate(t.SetMulColor, ratio, AssignmentType.Multiplicative);
        static public void GroupAnimateColorRatio(this TextMeshPro t, float ratio)
        => GroupAnimate(t.SetMulColor, ratio, AssignmentType.Multiplicative);
        static public void GroupAnimateAlphaRatio(this TextMeshPro t, float ratio)
        => GroupAnimate(t.SetMulAlpha, ratio, AssignmentType.Multiplicative);

        // Privates
        static private void SetAddColor(this TextMeshPro t, Color a)
        => t.color += a;
        static private void SetMulColor(this TextMeshPro t, Color a)
        => t.color *= a;
        static private void SetMulColor(this TextMeshPro t, float a)
        => t.color *= a;
        static private void SetAddAlpha(this TextMeshPro t, float a)
        => t.alpha += a;
        static private void SetMulAlpha(this TextMeshPro t, float a)
        => t.alpha *= a;
        static private Color ColorOffsetTo(this TextMeshPro t, Color a)
        => a - t.color;
        static private float AlphaOffsetTo(this TextMeshPro t, float a)
        => a - t.alpha;
        #endregion

        #region Optionals presets
        static public OptionalParameters Interrupt(this Component t)
        => new OptionalParameters
        {
            ConflictResolution = ConflictResolution.Interrupt,
            GUID = t,
        };
        static public OptionalParameters InterruptAndDeactivate(this Component t)
        => new OptionalParameters
        {
            ConflictResolution = ConflictResolution.Interrupt,
            GUID = t,
            EventInfo = new EventInfo(() => t.gameObject.SetActive(false)),
        };
        #endregion
    }
}
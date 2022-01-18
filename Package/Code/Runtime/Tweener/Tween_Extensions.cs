namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Tools.Extensions.Math;

    /// <summary> Shorthands for common component-specific property modifiers </summary>
    /// <remarks>
    /// Before calling any component-specific extension, the tween's <c><see cref="GameObject"/></c> must be initialized with either of the following: <br/>
    /// • <c><see cref="Tween.SetGameObject(GameObject)"/></c> <br/>
    /// • <c><see cref="NewTween(Component)"/></c> 
    /// </remarks>
    static public class Tween_Extensions
    {
        // General
        /// <summary> Creates a new tween with pre-initialized <c><see cref="GameObject"/></c> and <c>ConflictLayer</c> </summary>
        /// <param name="t"> 
        ///     The new tween's <c><see cref="GameObject"/></c> is set to this component's <c>gameObject</c>, <br/>
        ///     and <c>ConflictLayer</c> is set to this component's instance
        /// </param>
        static public Tween NewTween(this Component t)
        => Tweener.NewTween.SetGameObject(t.gameObject).SetConflictLayer(t);
        /// <summary> Stops all tweens on a conflict layer defined by this component's instance </summary>
        static public void StopTweens(this Component t)
        => Tweener.StopLayer(t);

        // Transform   
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Transform.position"/></c> to the chosen position </summary>
        static public Tween Position(this Tween t, Vector3 to)
        => t.AddModifier_Transform_Position(t.GameObject.transform, to);
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Transform.localPosition"/></c> to the chosen local position  </summary>
        static public Tween LocalPosition(this Tween t, Vector3 to)
        => t.AddModifier_Transform_LocalPosition(t.GameObject.transform, to);
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Transform.rotation"/></c> to the chosen rotation </summary>
        static public Tween Rotation(this Tween t, Quaternion to)
        => t.AddModifier_Transform_Rotation(t.GameObject.transform, to);
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Transform.localRotation"/></c> to the chosen local rotation </summary>
        static public Tween LocalRotation(this Tween t, Quaternion to)
        => t.AddModifier_Transform_LocalRotation(t.GameObject.transform, to);
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Transform.localScale"/></c> to the chosen local scale </summary>
        static public Tween LocalScale(this Tween t, Vector3 to)
        => t.AddModifier_Transform_LocalScale(t.GameObject.transform, to);
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Transform.localScale"/></c> by the chosen ratio </summary>
        static public Tween LocalScaleRatio(this Tween t, Vector3 ratio)
        => t.AddModifier_Transform_LocalScaleRatio(t.GameObject.transform, ratio);
        /// <inheritdoc cref="LocalScaleRatio(Tween, Vector3)"/>
        static public Tween LocalScaleRatio(this Tween t, float ratio)
        => t.AddModifier_Transform_LocalScaleRatio(t.GameObject.transform, ratio);

        static private Tween AddModifier_Transform_Position(this Tween t, Transform c, Vector3 to)
        => t.AddPropertyModifier(v => c.position += v, to - c.position);
        static private Tween AddModifier_Transform_LocalPosition(this Tween t, Transform c, Vector3 to)
        => t.AddPropertyModifier(v => c.localPosition += v, to - c.localPosition);
        static private Tween AddModifier_Transform_Rotation(this Tween t, Transform c, Quaternion to)
        => t.AddPropertyModifier(v => c.rotation *= v, to.Sub(c.rotation));
        static private Tween AddModifier_Transform_LocalRotation(this Tween t, Transform c, Quaternion to)
        => t.AddPropertyModifier(v => c.localRotation *= v, to.Sub(c.localRotation));
        static private Tween AddModifier_Transform_LocalScale(this Tween t, Transform c, Vector3 to)
        => t.AddPropertyModifier(v => c.localScale += v, to - c.localScale);
        static private Tween AddModifier_Transform_LocalScaleRatio(this Tween t, Transform c, Vector3 ratio)
        => t.AddPropertyModifier(v => c.localScale = c.localScale.Mul(v), ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_Transform_LocalScaleRatio(this Tween t, Transform c, float ratio)
        => t.AddPropertyModifier(v => c.localScale *= v, ratio, DeltaValueType.Ratio);

        // SpriteRenderer
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="SpriteRenderer.color"/></c> to the chosen color </summary>
        static public Tween SpriteColor(this Tween t, Color to)
        => t.AddModifier_SpriteRenderer_Color(t.GameObject.GetComponent<SpriteRenderer>(), to);
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s <c><see cref="SpriteRenderer.color"/></c> by the chosen ratio </summary>
        static public Tween SpriteColorRatio(this Tween t, Color ratio)
        => t.AddModifier_SpriteRenderer_ColorRatio(t.GameObject.GetComponent<SpriteRenderer>(), ratio);
        /// <inheritdoc cref="SpriteColorRatio(Tween, Color)"/>
        static public Tween SpriteColorRatio(this Tween t, float ratio)
        => t.AddModifier_SpriteRenderer_ColorRatio(t.GameObject.GetComponent<SpriteRenderer>(), ratio);
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="SpriteRenderer.color"/></c>'s alpha to the chosen alpha </summary>
        static public Tween SpriteAlpha(this Tween t, float to)
        => t.AddModifier_SpriteRenderer_Alpha(t.GameObject.GetComponent<SpriteRenderer>(), to);
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s <c><see cref="SpriteRenderer.color"/></c>'s alpha by the chosen ratio </summary>
        static public Tween SpriteAlphaRatio(this Tween t, float ratio)
        => t.AddModifier_SpriteRenderer_AlphaRatio(t.GameObject.GetComponent<SpriteRenderer>(), ratio);

        static private Tween AddModifier_SpriteRenderer_Color(this Tween t, SpriteRenderer c, Color to)
        => t.AddPropertyModifier(v => c.color += v, to - c.color);
        static private Tween AddModifier_SpriteRenderer_ColorRatio(this Tween t, SpriteRenderer c, Color ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_SpriteRenderer_ColorRatio(this Tween t, SpriteRenderer c, float ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_SpriteRenderer_Alpha(this Tween t, SpriteRenderer c, float to)
        => t.AddPropertyModifier(v => c.color += new Color(0f, 0f, 0f, v), to - c.color.a);
        static private Tween AddModifier_SpriteRenderer_AlphaRatio(this Tween t, SpriteRenderer c, float ratio)
        => t.AddPropertyModifier(v => c.color *= new Color(1f, 1f, 1f, v), ratio);

        // UI Image
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Image.color"/></c> to the chosen color </summary>
        static public Tween ImageColor(this Tween t, Color to)
        => t.AddModifier_Image_Color(t.GameObject.GetComponent<Image>(), to);
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Image.color"/></c> by the chosen ratio </summary>
        static public Tween ImageColorRatio(this Tween t, Color ratio)
        => t.AddModifier_Image_ColorRatio(t.GameObject.GetComponent<Image>(), ratio);
        /// <inheritdoc cref="ImageColorRatio(Tween, Color)"/>
        static public Tween ImageColorRatio(this Tween t, float ratio)
        => t.AddModifier_Image_ColorRatio(t.GameObject.GetComponent<Image>(), ratio);
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Image.color"/></c>'s alpha to the chosen alpha </summary>
        static public Tween ImageAlpha(this Tween t, float to)
        => t.AddModifier_Image_Alpha(t.GameObject.GetComponent<Image>(), to);
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s <c><see cref="Image.color"/></c>'s alpha by the chosen ratio </summary>
        static public Tween ImageAlphaRatio(this Tween t, float ratio)
        => t.AddModifier_Image_AlphaRatio(t.GameObject.GetComponent<Image>(), ratio);

        static private Tween AddModifier_Image_Color(this Tween t, Image c, Color to)
        => t.AddPropertyModifier(v => c.color += v, to - c.color);
        static private Tween AddModifier_Image_ColorRatio(this Tween t, Image c, Color ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_Image_ColorRatio(this Tween t, Image c, float ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_Image_Alpha(this Tween t, Image c, float to)
        => t.AddPropertyModifier(v => c.color += new Color(0f, 0f, 0f, v), to - c.color.a);
        static private Tween AddModifier_Image_AlphaRatio(this Tween t, Image c, float ratio)
        => t.AddPropertyModifier(v => c.color *= new Color(1f, 1f, 1f, v), ratio);

        // TextMeshPro
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="TMP_Text.color"/></c> to the chosen color </summary>
        static public Tween TMPColor(this Tween t, Color to)
        => t.AddModifier_TextMeshPro_Color(t.GameObject.GetComponent<TextMeshPro>(), to);
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s <c><see cref="TMP_Text.color"/></c> by the chosen ratio </summary>
        static public Tween TMPColorRatio(this Tween t, Color ratio)
        => t.AddModifier_TextMeshPro_ColorRatio(t.GameObject.GetComponent<TextMeshPro>(), ratio);
        /// <inheritdoc cref="TMPColorRatio(Tween, Color)"/>
        static public Tween TMPColorRatio(this Tween t, float ratio)
        => t.AddModifier_TextMeshPro_ColorRatio(t.GameObject.GetComponent<TextMeshPro>(), ratio);
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s <c><see cref="TMP_Text.color"/></c>'s alpha to the chosen alpha </summary>
        static public Tween TMPAlpha(this Tween t, float to)
        => t.AddModifier_TextMeshPro_Alpha(t.GameObject.GetComponent<TextMeshPro>(), to);
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s <c><see cref="TMP_Text.color"/></c>'s alpha by the chosen ratio </summary>
        static public Tween TMPAlphaRatio(this Tween t, float ratio)
        => t.AddModifier_TextMeshPro_AlphaRatio(t.GameObject.GetComponent<TextMeshPro>(), ratio);

        static private Tween AddModifier_TextMeshPro_Color(this Tween t, TextMeshPro c, Color to)
        => t.AddPropertyModifier(v => c.color += v, to - c.color);
        static private Tween AddModifier_TextMeshPro_ColorRatio(this Tween t, TextMeshPro c, Color ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_TextMeshPro_ColorRatio(this Tween t, TextMeshPro c, float ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_TextMeshPro_Alpha(this Tween t, TextMeshPro c, float to)
        => t.AddPropertyModifier(v => c.alpha += v, to - c.color.a);
        static private Tween AddModifier_TextMeshPro_AlphaRatio(this Tween t, TextMeshPro c, float ratio)
        => t.AddPropertyModifier(v => c.alpha *= v, ratio);
    }
}
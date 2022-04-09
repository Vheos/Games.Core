namespace Vheos.Games.Core
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
    /// • <c><see cref="NewTween(Component)"/></c> <br/>
    /// • <c><see cref="NewTweenConflict(Component)"/></c> 
    /// </remarks>
    static public class Tween_Extensions
    {
        // General
        /// <summary> Creates a new tween with pre-initialized <c><see cref="GameObject"/></c> </summary>
        /// <param name="t"> 
        ///     The new tween's <c><see cref="GameObject"/></c> is set to this component's <c>gameObject</c>
        /// </param>
        static public Tween NewTween(this Component t)
        => Tweener.NewTween.SetGameObject(t.gameObject);
        /// <summary> Creates a new tween with pre-initialized <c><see cref="GameObject"/></c>, <c>ConflictLayer</c> and chosen <c><see cref="ConflictResolution"/></c> </summary>
        /// <param name="t"> 
        ///     This component's <c>gameObject</c> is assigned to the new tween's <c><see cref="GameObject"/></c>, <br/>
        ///     and this component's instance is assigned to its <c>ConflictLayer</c>
        /// </param>
        /// <param name="conflictResolution"> The new tween's <c><see cref="ConflictResolution"/></c> </param>
        static public Tween NewTween(this Component t, ConflictResolution conflictResolution)
        => Tweener.NewTween.SetGameObject(t.gameObject).SetConflictLayer(t).SetConflictResolution(conflictResolution);
        /// <summary> Stops all tweens on a conflict layer defined by this component's instance </summary>
        static public void StopLayerTweens(this Component t)
        => Tweener.StopLayer(t);
        /// <summary> Stops all tweens which are modifying this component's gameObject </summary>
        static public void StopGameObjectTweens(this Component t)
        => Tweener.StopGameObject(t.gameObject);
        static public ColorComponent FindColorComponent(this GameObject t)
        {
            foreach (var component in t.GetComponents<Component>())
                switch (component)
                {
                    case SpriteRenderer _: return ColorComponent.SpriteRenderer;
                    case TextMeshPro _: return ColorComponent.TextMeshPro;
                    case Image _: return ColorComponent.Image;
                }
            return ColorComponent.None;
        }
        static public ColorComponent FindColorComponent(this Component t)
        => t.gameObject.FindColorComponent();
        static public Tween SetInterrupt(this Tween t)
        => t.SetConflictResolution(ConflictResolution.Interrupt);

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

        // Color
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s color to the chosen value </summary>
        static public Tween Color(this Tween t, ColorComponent colorComponentType, Color to)
        => colorComponentType switch
        {
            ColorComponent.Any => t.Color(t.GameObject.FindColorComponent(), to),
            ColorComponent.SpriteRenderer => t.AddModifier_Color(t.GameObject.GetComponent<SpriteRenderer>(), to),
            ColorComponent.TextMeshPro => t.AddModifier_Color(t.GameObject.GetComponent<TextMeshPro>(), to),
            ColorComponent.Image => t.AddModifier_Color(t.GameObject.GetComponent<Image>(), to),
            _ => t,
        };
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s color by the chosen ratio </summary>
        static public Tween ColorRatio(this Tween t, ColorComponent colorComponentType, Vector4 ratio)
        => colorComponentType switch
        {
            ColorComponent.Any => t.ColorRatio(t.GameObject.FindColorComponent(), ratio),
            ColorComponent.SpriteRenderer => t.AddModifier_ColorRatio(t.GameObject.GetComponent<SpriteRenderer>(), ratio),
            ColorComponent.TextMeshPro => t.AddModifier_ColorRatio(t.GameObject.GetComponent<TextMeshPro>(), ratio),
            ColorComponent.Image => t.AddModifier_ColorRatio(t.GameObject.GetComponent<Image>(), ratio),
            _ => t,
        };
        /// <inheritdoc cref="ColorRatio(Tween, ColorComponent, float)"/>
        static public Tween ColorRatio(this Tween t, ColorComponent colorComponentType, float ratio)
        => colorComponentType switch
        {
            ColorComponent.Any => t.ColorRatio(t.GameObject.FindColorComponent(), ratio),
            ColorComponent.SpriteRenderer => t.AddModifier_ColorRatio(t.GameObject.GetComponent<SpriteRenderer>(), ratio),
            ColorComponent.TextMeshPro => t.AddModifier_ColorRatio(t.GameObject.GetComponent<TextMeshPro>(), ratio),
            ColorComponent.Image => t.AddModifier_ColorRatio(t.GameObject.GetComponent<Image>(), ratio),
            _ => t,
        };
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s rgb to the chosen value </summary>
        static public Tween RGB(this Tween t, ColorComponent colorComponentType, Color to)
        => colorComponentType switch
        {
            ColorComponent.Any => t.RGB(t.GameObject.FindColorComponent(), to),
            ColorComponent.SpriteRenderer => t.AddModifier_RGB(t.GameObject.GetComponent<SpriteRenderer>(), to),
            ColorComponent.TextMeshPro => t.AddModifier_RGB(t.GameObject.GetComponent<TextMeshPro>(), to),
            ColorComponent.Image => t.AddModifier_RGB(t.GameObject.GetComponent<Image>(), to),
            _ => t,
        };
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s rgb by the chosen ratio </summary>
        static public Tween RGBRatio(this Tween t, ColorComponent colorComponentType, Vector3 ratio)
        => colorComponentType switch
        {
            ColorComponent.Any => t.RGBRatio(t.GameObject.FindColorComponent(), ratio),
            ColorComponent.SpriteRenderer => t.AddModifier_RGBRatio(t.GameObject.GetComponent<SpriteRenderer>(), ratio),
            ColorComponent.TextMeshPro => t.AddModifier_RGBRatio(t.GameObject.GetComponent<TextMeshPro>(), ratio),
            ColorComponent.Image => t.AddModifier_RGBRatio(t.GameObject.GetComponent<Image>(), ratio),
            _ => t,
        };
        /// <inheritdoc cref="RGBRatio(Tween, ColorComponent, Vector3)"/>
        static public Tween RGBRatio(this Tween t, ColorComponent colorComponentType, float ratio)
        => colorComponentType switch
        {
            ColorComponent.Any => t.RGBRatio(t.GameObject.FindColorComponent(), ratio),
            ColorComponent.SpriteRenderer => t.AddModifier_RGBRatio(t.GameObject.GetComponent<SpriteRenderer>(), ratio),
            ColorComponent.TextMeshPro => t.AddModifier_RGBRatio(t.GameObject.GetComponent<TextMeshPro>(), ratio),
            ColorComponent.Image => t.AddModifier_RGBRatio(t.GameObject.GetComponent<Image>(), ratio),
            _ => t,
        };
        /// <summary> Offsets this tween's <c><see cref="GameObject"/></c>'s alpha to the chosen value </summary>
        static public Tween Alpha(this Tween t, ColorComponent colorComponentType, float to)
        => colorComponentType switch
        {
            ColorComponent.Any => t.Alpha(t.GameObject.FindColorComponent(), to),
            ColorComponent.SpriteRenderer => t.AddModifier_Alpha(t.GameObject.GetComponent<SpriteRenderer>(), to),
            ColorComponent.TextMeshPro => t.AddModifier_Alpha(t.GameObject.GetComponent<TextMeshPro>(), to),
            ColorComponent.Image => t.AddModifier_Alpha(t.GameObject.GetComponent<Image>(), to),
            _ => t,
        };
        /// <summary> Scales this tween's <c><see cref="GameObject"/></c>'s alpha by the chosen ratio </summary>
        static public Tween AlphaRatio(this Tween t, ColorComponent colorComponentType, float ratio)
        => colorComponentType switch
        {
            ColorComponent.Any => t.AlphaRatio(t.GameObject.FindColorComponent(), ratio),
            ColorComponent.SpriteRenderer => t.AddModifier_AlphaRatio(t.GameObject.GetComponent<SpriteRenderer>(), ratio),
            ColorComponent.TextMeshPro => t.AddModifier_AlphaRatio(t.GameObject.GetComponent<TextMeshPro>(), ratio),
            ColorComponent.Image => t.AddModifier_AlphaRatio(t.GameObject.GetComponent<Image>(), ratio),
            _ => t,
        };

        // SpriteRenderer
        static private Tween AddModifier_Color(this Tween t, SpriteRenderer c, Color to)
        => t.AddPropertyModifier(v => c.color += v, to - c.color);
        static private Tween AddModifier_ColorRatio(this Tween t, SpriteRenderer c, Vector4 ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_ColorRatio(this Tween t, SpriteRenderer c, float ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_RGB(this Tween t, SpriteRenderer c, Color to)
        => t.AddPropertyModifier(v => c.color += v, to.Sub(c.color).NewA(0f));
        static private Tween AddModifier_RGBRatio(this Tween t, SpriteRenderer c, Vector3 ratio)
        => t.AddPropertyModifier(v => c.color *= v.Append(1f), ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_RGBRatio(this Tween t, SpriteRenderer c, float ratio)
        => t.AddPropertyModifier(v => c.color *= new Vector4(v, v, v, 1f), ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_Alpha(this Tween t, SpriteRenderer c, float to)
        => t.AddPropertyModifier(v => c.color += new Color(0f, 0f, 0f, v), to - c.color.a);
        static private Tween AddModifier_AlphaRatio(this Tween t, SpriteRenderer c, float ratio)
        => t.AddPropertyModifier(v => c.color *= new Color(1f, 1f, 1f, v), ratio, DeltaValueType.Ratio);

        // UI Image
        static private Tween AddModifier_Color(this Tween t, Image c, Color to)
        => t.AddPropertyModifier(v => c.color += v, to - c.color);
        static private Tween AddModifier_ColorRatio(this Tween t, Image c, Vector4 ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_ColorRatio(this Tween t, Image c, float ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_RGB(this Tween t, Image c, Color to)
        => t.AddPropertyModifier(v => c.color += v, to.Sub(c.color).NewA(0f));
        static private Tween AddModifier_RGBRatio(this Tween t, Image c, Vector3 ratio)
        => t.AddPropertyModifier(v => c.color *= v.Append(1f), ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_RGBRatio(this Tween t, Image c, float ratio)
        => t.AddPropertyModifier(v => c.color *= new Vector4(v, v, v, 1f), ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_Alpha(this Tween t, Image c, float to)
        => t.AddPropertyModifier(v => c.color += new Color(0f, 0f, 0f, v), to - c.color.a);
        static private Tween AddModifier_AlphaRatio(this Tween t, Image c, float ratio)
        => t.AddPropertyModifier(v => c.color *= new Color(1f, 1f, 1f, v), ratio, DeltaValueType.Ratio);

        // TextMeshPro
        static private Tween AddModifier_Color(this Tween t, TextMeshPro c, Color to)
        => t.AddPropertyModifier(v => c.color += v, to - c.color);
        static private Tween AddModifier_ColorRatio(this Tween t, TextMeshPro c, Vector4 ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_ColorRatio(this Tween t, TextMeshPro c, float ratio)
        => t.AddPropertyModifier(v => c.color *= v, ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_RGB(this Tween t, TextMeshPro c, Color to)
        => t.AddPropertyModifier(v => c.color += v, to.Sub(c.color).NewA(0f));
        static private Tween AddModifier_RGBRatio(this Tween t, TextMeshPro c, Vector3 ratio)
        => t.AddPropertyModifier(v => c.color *= v.Append(1f), ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_RGBRatio(this Tween t, TextMeshPro c, float ratio)
        => t.AddPropertyModifier(v => c.color *= new Vector4(v, v, v, 1f), ratio, DeltaValueType.Ratio);
        static private Tween AddModifier_Alpha(this Tween t, TextMeshPro c, float to)
        => t.AddPropertyModifier(v => c.alpha += v, to - c.color.a);
        static private Tween AddModifier_AlphaRatio(this Tween t, TextMeshPro c, float ratio)
        => t.AddPropertyModifier(v => c.alpha *= v, ratio, DeltaValueType.Ratio);
    }
}
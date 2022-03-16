namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(SpriteChangable))]
    abstract public class ASpriteRendererAutoMProps : AAutoMProps
    {
        // Constants
        private const string SPRITE_RENDERER_INTERNAL_TEXTURE_NAME = "_MainTex";

        // Publics
        public Texture2D InternalSpriteTexture
        {
            get => GetTexture(SPRITE_RENDERER_INTERNAL_TEXTURE_NAME) as Texture2D;
            set => SetTexture(SPRITE_RENDERER_INTERNAL_TEXTURE_NAME, value);
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            Get<SpriteChangable>().OnChangeSprite.SubEnableDisable(this, (from, to) => InternalSpriteTexture = to?.texture);
        }
    }
}
namespace Vheos.Games.Core
{
    using UnityEngine;

    [RequireComponent(typeof(Updatable))]
    [DisallowMultipleComponent]
    sealed public class SpriteChangable : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Sprite, Sprite> OnChangeSprite = new();

        // Privates
        private Sprite _previousSprite;
        private void TryInvokeEvents()
        {
            Sprite currentSprite = Get<SpriteRenderer>().sprite;
            if (_previousSprite != currentSprite)
                OnChangeSprite?.Invoke(_previousSprite, currentSprite);
            _previousSprite = currentSprite;
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            Get<Updatable>().OnUpdate.SubscribeAuto(this, TryInvokeEvents);
        }
    }
}
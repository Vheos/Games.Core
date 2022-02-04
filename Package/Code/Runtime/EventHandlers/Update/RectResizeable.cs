namespace Vheos.Games.Core
{
    using UnityEngine;
    
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Updatable))]
    [DisallowMultipleComponent]
    sealed public class RectResizeable : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Vector2, Vector2> OnResize = new();

        // Privates
        private RectTransform _rectTransform;
        private Vector2 _previousSize;
        private void TryInvokeEvents()
        {
            Vector2 currentSize = _rectTransform.rect.size;
            if (currentSize != _previousSize)
                OnResize.Invoke(_previousSize, currentSize);            

            _previousSize = currentSize;
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _rectTransform = Get<RectTransform>();
            Get<Updatable>().OnUpdate.SubEnableDisable(this, TryInvokeEvents);
        }
        protected override void PlayEnable()
        {
            base.PlayEnable();
            _previousSize = _rectTransform.rect.size;
        }
    }
}
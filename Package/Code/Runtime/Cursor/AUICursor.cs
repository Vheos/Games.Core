namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using Tools.Extensions.Math;
    using Tools.Extensions.UnityObjects;
    using Tools.Extensions.General;

    [RequireComponent(typeof(Updatable))]
    [DisallowMultipleComponent]
    abstract public class AUICursor : ABaseComponent
    {
        // Inspector
        [Header("Gameplay")]
        [SerializeField] [Range(0f, 2f)] protected float _Sensitivity = 1f;

        // Privates
        private Canvas _canvas;
        private Camera _camera;
        private AUICursorable _cursorable;
        internal bool IsOverCursorable(AUICursorable cursorable)
        => cursorable.Trigger.Raycast(_camera.ScreenPointToRay(Get<RectTransform>().anchoredPosition), out _, float.PositiveInfinity);
        private AUICursorable FindClosestCursorable()
        {
            if (!_camera.isActiveAndEnabled)
                return null;

            AUICursorable closestCursorable = null;
            float minDistance = float.PositiveInfinity;
            foreach (var cursorable in AUICursorableManager.ActiveComponents)
                if (cursorable.DistanceTo(_camera) < minDistance
                && cursorable.Trigger.Raycast(_camera.ScreenPointToRay(Get<RectTransform>().anchoredPosition), out var hitInfo, float.PositiveInfinity)
                && cursorable.PerformRaycastTests(hitInfo.point))
                {
                    closestCursorable = cursorable;
                    minDistance = cursorable.DistanceTo(_camera);
                }
            return closestCursorable;
        }
        virtual protected void OnInputMoveCursor(Vector2 offset)
        => transform.position = transform.position.Add(offset * _Sensitivity).Clamp(Vector2.zero, _canvas.renderingDisplaySize);
        virtual protected void OnInputPressConfirm()
        {
            if (_cursorable != null)
                _cursorable.TryInvokeOnPress(this);
        }
        virtual protected void OnInputReleaseConfirm()
        {
            if (_cursorable != null)
                _cursorable.TryInvokeOnRelease(this);
        }
        protected void OnUpdate()
        {
            if (_cursorable != null
            && _cursorable.TryInvokeOnHold(this))
                return;

            var previousCursorable = _cursorable;
            _cursorable = FindClosestCursorable().ChooseIf(t => t != null && !t.IsHeld);
            if (_cursorable == previousCursorable)
                return;

            if (previousCursorable != null)
                previousCursorable.TryInvokeOnLoseHighlight(this);
            if (_cursorable != null)
                _cursorable.TryInvokeOnGainHighlight(this);

        }

        // Play
        public void Initialize(Canvas canvas, Camera camera)
        {
            Get<Updatable>().OnUpdate.SubscribeAuto(this, OnUpdate);
            _canvas = canvas;
            _camera = camera;
            this.BecomeChildOf(_canvas);
            transform.position = _canvas.renderingDisplaySize / 2f;
        }
    }
}
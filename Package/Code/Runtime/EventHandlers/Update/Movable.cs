namespace Vheos.Games.Core
{
    using UnityEngine;
    
    [RequireComponent(typeof(Updatable))]
    [DisallowMultipleComponent]
    sealed public class Movable : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Vector3> OnStartMoving = new();
        public readonly AutoEvent<Vector3, Vector3> OnMove = new();
        public readonly AutoEvent<Vector3> OnStop = new();

        // Privates
        private Vector3 _previousPosition;
        private bool _previousHasMoved;
        private void TryInvokeEvents()
        {
            Vector3 currentPosition = transform.position;
            bool currentHasMoved = currentPosition != _previousPosition;

            if (currentHasMoved)
            {
                if (!_previousHasMoved)
                    OnStartMoving.Invoke(_previousPosition);
                OnMove.Invoke(_previousPosition, currentPosition);
            }
            else if (_previousHasMoved)
                OnStop.Invoke(currentPosition);

            _previousPosition = currentPosition;
            _previousHasMoved = currentHasMoved;
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            Get<Updatable>().OnUpdate.SubEnableDisable(this, TryInvokeEvents);
        }
        protected override void PlayEnable()
        {
            base.PlayEnable();
            _previousPosition = transform.position;
            _previousHasMoved = false;
        }
    }
}
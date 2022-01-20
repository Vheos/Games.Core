namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class UICursorManager : AComponentManager<UICursorManager, AUICursor>
    {
        // Inspector
        [SerializeField] protected bool _DisableNativeCursor;

        protected override void PlayAwake()
        {
            base.PlayAwake();
            if (_DisableNativeCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
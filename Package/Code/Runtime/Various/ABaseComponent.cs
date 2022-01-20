namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    /// <summary> Base class for all user-made components. Wraps component-related methods </summary>
    /// <remarks> If <b><c>EDIT_MODE_CALLBACKS</c></b> is defined, will derive from <c><see cref="AEditable"/></c> and provide callbacks for edit mode events </remarks>
    abstract public class ABaseComponent :
#if EDIT_MODE_CALLBACKS
        AEditable
#else
        Playable
#endif
    {
        // Publics
        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
        /// <summary> Adds component of type <typeparamref name="T"/> to current <c><see cref="GameObject"/></c>. </summary>
        /// <returns> The component that has just been added.</returns>
        public T Add<T>() where T : Component
        => gameObject.AddComponent<T>();
        /// <summary> Gets component of type <typeparamref name="T"/> (or derived) attached to current <c><see cref="GameObject"/></c>. </summary>
        /// <returns>
        /// The component, if it was found.<br/>
        /// <c><see langword="null"/></c> otherwise.
        /// </returns>
        public T Get<T>() where T : Component
        => GetComponent<T>();
        /// <summary> Checks whether current <c><see cref="GameObject"/></c> contains component of type <typeparamref name="T"/> (or derived).</summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the component was found.<br/>
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        public bool Has<T>() where T : Component
        => GetComponent<T>() != null;
        /// <summary> Gets component of type <typeparamref name="T"/> (or derived) attached to current <c><see cref="GameObject"/></c> and assigns it to <c><see langword="out"/></c> <c><paramref name="component"/></c>. </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the component was found and assigned to <c><see langword="out"/></c> <c><paramref name="component"/></c>. <br/>
        /// <c><see langword="false"/></c> otherwise. In such case, <c><see langword="out"/></c> <c><paramref name="component"/></c> will be set to <c><see langword="null"/></c>.
        /// </returns>
        public bool TryGet<T>(out T component) where T : Component
        => TryGetComponent(out component);
        /// <summary> Gets component of type <typeparamref name="T"/> (or derived) attached to current <c><see cref="GameObject"/></c> or adds one, if none was found. </summary>
        /// <returns>
        /// The component, whether it has already been attached or just added.
        /// </returns>
        public T GetOrAdd<T>() where T : Component
        => TryGetComponent<T>(out var component) ? component : Add<T>();

        // Privates
        internal bool IsEnabled
        { get; private set; }

        // Play
        protected override void PlayEnable()
        {
            IsEnabled = true;
            base.PlayEnable();
        }
        protected override void PlayDisable()
        {
            IsEnabled = false;
            base.PlayDisable();
        }
    }
}
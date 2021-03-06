namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.UnityObjects;

    /// <summary> Base class for all user-made components. Wraps component-related methods </summary>
    /// <remarks> If <b><c>EDIT_MODE_CALLBACKS</c></b> is defined, will derive from <c><see cref="AEditable"/></c> and provide callbacks for edit mode events </remarks>
    abstract public class ABaseComponent :
#if EDIT_MODE_CALLBACKS
        AEditable
#else
        Playable
#endif
    {
        // Publics - BindEvents
        public void BindEnableDisable(ABaseComponent a)
        {
            a.OnPlayEnable.SubDestroy(this, Enable);
            a.OnPlayDisable.SubDestroy(this, Disable);
        }
        public void BindDestroyObject(ABaseComponent a)
        => a.OnPlayDestroy.SubDestroy(this, this.DestroyObject);

        // Publics - Active
        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
        public void Activate()
        => gameObject.SetActive(true);
        public void Deactivate()
        => gameObject.SetActive(false);

        // Publics - Enable
        public bool IsEnabled
        {
            get => enabled;
            set => enabled = value;
        }
        public void Enable()
        => enabled = true;
        public void Disable()
        => enabled = false;

        // Publics - Component, Generic
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

        // Publics - Component, Type
        public Component Add(Type type)
        => gameObject.AddComponent(type);
        public Component Get(Type type)
        => GetComponent(type);
        public bool Has(Type type)
        => GetComponent(type) != null;
        public bool TryGet(Type type, out Component component)
        => TryGetComponent(type, out component);
        public Component GetOrAdd(Type type)
        => TryGetComponent(type, out var component) ? component : Add(type);

        // Statics - Component
        static protected T Add<T>(ABaseComponent baseComponent) where T : Component
        => baseComponent.Add<T>();
        static protected T Get<T>(ABaseComponent baseComponent) where T : Component
        => baseComponent.Get<T>();
        static protected bool Has<T>(ABaseComponent baseComponent) where T : Component
        => baseComponent.Has<T>();
        static protected bool TryGet<T>(ABaseComponent baseComponent, out T component) where T : Component
        => baseComponent.TryGet(out component);
        static protected T GetOrAdd<T>(ABaseComponent baseComponent) where T : Component
        => baseComponent.GetOrAdd<T>();

        // Internals
        internal bool IsBetweenEnableAndDisable
        { get; private set; }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            AStaticManager.TryRegisterComponent(this);
        }
        protected override void PlayEnable()
        {
            IsBetweenEnableAndDisable = true;
            base.PlayEnable();
        }
        protected override void PlayDisable()
        {
            IsBetweenEnableAndDisable = false;
            base.PlayDisable();
        }
    }
}
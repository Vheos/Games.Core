namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    using Tools.Extensions.UnityObjects;
    using System.Collections.Generic;

    /// <summary> Base class for all user-made components. Wraps component-related methods. </summary>
    /// <remarks>
    /// Can be extended by defining the following compilation symbols:<br/>
    /// • <b><c>UNITY_EDITOR</c></b> - will derive from <c><see cref="AEditable"/></c> and provide callbacks for edit mode events<br/>
    /// • <b><c>CACHED_COMPONENTS</c></b> - will wrap <c><see cref="ComponentCache"/></c>'s component-related methods<br/>
    /// </remarks>
    abstract public class ABaseComponent :
#if UNITY_EDITOR
        AEditable
#else
        APlayable
#endif
    {

#if CACHED_COMPONENTS
        // Publics
        /// <summary> Adds component of type <typeparamref name="T"/> to current <c><see cref="GameObject"/></c>. </summary>
        /// <remarks>
        /// Also adds it to <c><see cref="ComponentCache"/></c>.<br/>
        /// If type <typeparamref name="T"/> (exact) has been cached before, overwrites the cached component with the newly-added one.
        /// </remarks>
        /// <returns> The component that has just been added.</returns>
        public T Add<T>() where T : Component
        => _componentCache.Add<T>();
        /// <summary> Gets component of type <typeparamref name="T"/> (exact) attached to current <c><see cref="GameObject"/></c>. </summary>
        /// <remarks> 
        /// Only the most-recently-cached component of type <typeparamref name="T"/> (exact) can be retrieved.<br/>
        /// If <c><see cref="ComponentCache"/></c> doesn't contain cached component of type <typeparamref name="T"/> (exact), throws <c><see cref="KeyNotFoundException"/></c>.
        /// </remarks>
        /// <returns>
        /// The component, if it was found.<br/>
        /// Throws <c><see cref="KeyNotFoundException"/></c> otherwise.
        /// </returns>
        public T Get<T>() where T : Component
        => _componentCache.Get<T>();
        /// <summary> Checks whether current <c><see cref="GameObject"/></c> contains component of type <typeparamref name="T"/> (exact).</summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the component was found.<br/>
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        public bool Has<T>() where T : Component
        => _componentCache.Has<T>();
        /// <summary> Gets component of type <typeparamref name="T"/> (exact) attached to current <c><see cref="GameObject"/></c> and assigns it to <c><see langword="out"/></c> <c><paramref name="component"/></c>. </summary>
        /// <remarks>  Only the most-recently-cached component of type <typeparamref name="T"/> (exact) can be retrieved. </remarks>
        /// <returns>
        /// <c><see langword="true"/></c> if the component was found and assigned to <c><see langword="out"/></c> <c><paramref name="component"/></c>. <br/>
        /// <c><see langword="false"/></c> otherwise. In such case, <c><see langword="out"/></c> <c><paramref name="component"/></c> will be set to <c><see langword="null"/></c>.
        /// </returns>
        public bool TryGet<T>(out T component) where T : Component
        => _componentCache.TryGet(out component);
        /// <summary> Gets component of type <typeparamref name="T"/> (exact) attached to current <c><see cref="GameObject"/></c> or adds one, if none was found. </summary>
        /// <returns>
        /// The component, whether it has already been attached or just added.
        /// </returns>
        public T GetOrAdd<T>() where T : Component
        => _componentCache.GetOrAdd<T>();
        /// <summary> Adds component of type <typeparamref name="T"/> (or derived) attached to current <c><see cref="GameObject"/></c> to <c><see cref="ComponentCache"/></c>.</summary>
        /// <remarks> 
        /// Used to manually define <c><see cref="ComponentCache"/></c> entries for base classes of existing components.<br/>
        /// Only use within <c><see cref="DefineCachedComponents"/></c>.
        /// </remarks>
        public void TryAddToCache<T>()
        => _componentCache.TryAddToCache<T>();

        // Privates
        private ComponentCache _componentCache;
        /// <summary> Provides a safe timing point for using <c><see cref="TryAddToCache{T}"/></c> .</summary>
        virtual protected void DefineCachedComponents()
        { }

        // Play
        /// <inheritdoc cref="APlayable.PlayAwake"/>
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _componentCache = this.GetOrAddComponent<ComponentCache>();
            DefineCachedComponents();
        }
#else
        // Publics (generic)
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
#endif
    }
}



/*
// Publics (Component)
public Component Add(Type type)
=> _componentCache.Add(type);
public Component Get(Type type)
=> _componentCache.Get(type);
public bool Has(Type type)
=> _componentCache.Has(type);
public bool TryGet(Type type, out Component component)
=> _componentCache.TryGet(type, out component);
public Component GetOrNull(Type type)
=> _componentCache.GetOrNull(type);
public Component GetOrAdd(Type type)
=> _componentCache.GetOrAdd(type);

public void TryAddToCache(Type type)
=> _componentCache.TryAddToCache(type);
*/
/*
// Publics (Component)
public Component Add(Type type)
=> gameObject.AddComponent(type);
public Component Get(Type type)
=> GetComponent(type);
public bool Has(Type type)
=> GetComponent(type) != null;
public bool TryGet(Type type, out Component component)
=> TryGetComponent(type, out component);
public Component GetOrNull(Type type)
=> TryGetComponent(type, out var component) ? component : null;
public Component GetOrAdd(Type type)
=> TryGetComponent(type, out var component) ? component : Add(type);
*/
namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    using Tools.Extensions.UnityObjects;
    using System.Collections.Generic;

    /// <summary> Base class for all user-made components. Wraps component-related methods. </summary>
    /// <remarks>
    /// Can be extended by defining compilation symbols:<br/>
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
        /// <summary> Adds component of type <typeparamref name="T"/> to current <see cref="GameObject"/>. </summary>
        /// <remarks>
        /// Also adds it to <see cref="ComponentCache"/>.<br/>
        /// If type <typeparamref name="T"/> (exact) has been cached before, overwrites the cached component with the newly-added one.
        /// </remarks>
        /// <returns> The component that has just been added.</returns>
        public T Add<T>() where T : Component
        => _componentCache.Add<T>();
        /// <summary> Gets component of type <typeparamref name="T"/> (exact) attached to current <see cref="GameObject"/>. </summary>
        /// <remarks> 
        /// Only the most-recently-cached component of type <typeparamref name="T"/> (exact) can be retrieved.<br/>
        /// If <see cref="ComponentCache"/> doesn't contain cached component of type <typeparamref name="T"/> (exact), throws <see cref="KeyNotFoundException"/>.
        /// </remarks>
        /// <returns>
        /// The component, if it was found.<br/>
        /// Throws <see cref="KeyNotFoundException"/> otherwise.
        /// </returns>
        public T Get<T>() where T : Component
        => _componentCache.Get<T>();
        /// <summary> Checks whether current <see cref="GameObject"/> contains component of type <typeparamref name="T"/> (exact).</summary>
        /// <returns>
        /// <see langword="true"/> if the component was found.<br/>
        /// <see langword="false"/> otherwise.
        /// </returns>
        public bool Has<T>() where T : Component
        => _componentCache.Has<T>();
        /// <summary> Gets component of type <typeparamref name="T"/> (exact) attached to current <see cref="GameObject"/> and assigns it to <see langword="out"/> <paramref name="component"/>. </summary>
        /// <remarks>  Only the most-recently-cached component of type <typeparamref name="T"/> (exact) can be retrieved. </remarks>
        /// <returns>
        /// <see langword="true"/> if the component was found and assigned to <see langword="out"/> <paramref name="component"/>. <br/>
        /// <see langword="false"/> otherwise. In such case, <see langword="out"/> <paramref name="component"/> will be set to <see langword="null"/>.
        /// </returns>
        public bool TryGet<T>(out T component) where T : Component
        => _componentCache.TryGet(out component);
        /// <summary> Gets component of type <typeparamref name="T"/> (exact) attached to current <see cref="GameObject"/> or adds one, if none was found. </summary>
        /// <returns>
        /// The component, whether it has already been attached or just added.
        /// </returns>
        public T GetOrAdd<T>() where T : Component
        => _componentCache.GetOrAdd<T>();
        /// <summary> Adds component of type <typeparamref name="T"/> (or derived) attached to current <see cref="GameObject"/> to <see cref="ComponentCache"/>.</summary>
        /// <remarks> 
        /// Used to manually define <see cref="ComponentCache"/> entries for base classes of existing components.<br/>
        /// Only use within <see cref="DefineCachedComponents"/>.
        /// </remarks>
        public void TryAddToCache<T>()
        => _componentCache.TryAddToCache<T>();

        // Privates
        private ComponentCache _componentCache;
        /// <summary> Provides a safe timing point for using <see cref="TryAddToCache{T}"/> .</summary>
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
        /// <summary> Adds component of type <typeparamref name="T"/> to current <see cref="GameObject"/>. </summary>
        /// <returns> The component that has just been added.</returns>
        public T Add<T>() where T : Component
        => gameObject.AddComponent<T>();
        /// <summary> Gets component of type <typeparamref name="T"/> (or derived) attached to current <see cref="GameObject"/>. </summary>
        /// <returns>
        /// The component, if it was found.<br/>
        /// <see langword="null"/> otherwise.
        /// </returns>
        public T Get<T>() where T : Component
        => GetComponent<T>();
        /// <summary> Checks whether current <see cref="GameObject"/> contains component of type <typeparamref name="T"/> (or derived).</summary>
        /// <returns>
        /// <see langword="true"/> if the component was found.<br/>
        /// <see langword="false"/> otherwise.
        /// </returns>
        public bool Has<T>() where T : Component
        => GetComponent<T>() != null;
        /// <summary> Gets component of type <typeparamref name="T"/> (or derived) attached to current <see cref="GameObject"/> and assigns it to <see langword="out"/> <paramref name="component"/>. </summary>
        /// <returns>
        /// <see langword="true"/> if the component was found and assigned to <see langword="out"/> <paramref name="component"/>. <br/>
        /// <see langword="false"/> otherwise. In such case, <see langword="out"/> <paramref name="component"/> will be set to <see langword="null"/>.
        /// </returns>
        public bool TryGet<T>(out T component) where T : Component
        => TryGetComponent(out component);
        /// <summary> Gets component of type <typeparamref name="T"/> (or derived) attached to current <see cref="GameObject"/> or adds one, if none was found. </summary>
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
namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;
    using Tools.Extensions.UnityObjects;

    abstract public class ABaseComponent :
#if UNITY_EDITOR
        AEditable
#else
        APlayable
#endif
    {
#if CACHED_COMPONENTS
        // Publics (generic)
        public T Add<T>() where T : Component
        => _componentCache.Add<T>();
        public T Get<T>() where T : Component
        => _componentCache.Get<T>();
        public bool Has<T>() where T : Component
        => _componentCache.Has<T>();
        public bool TryGet<T>(out T component) where T : Component
        => _componentCache.TryGet(out component);
        public T GetOrNull<T>() where T : Component
        => _componentCache.GetOrNull<T>();
        public T GetOrAdd<T>() where T : Component
        => _componentCache.GetOrAdd<T>();

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

        // Publics (cache)
        public void TryAddToCache<T>()
        => _componentCache.TryAddToCache<T>();
        public void TryAddToCache(Type type)
        => _componentCache.TryAddToCache(type);

        // Privates
        private ComponentCache _componentCache;
        virtual protected void DefineCachedComponents()
        { }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _componentCache = this.GetOrAddComponent<ComponentCache>();
            DefineCachedComponents();
        }
#else
        // Publics (generic)
        public T Add<T>() where T : Component
        => gameObject.AddComponent<T>();
        public T Get<T>() where T : Component
        => GetComponent<T>();
        public bool Has<T>() where T : Component
        => GetComponent<T>() != null;
        public bool TryGet<T>(out T component) where T : Component
        => TryGetComponent(out component);
        public T GetOrNull<T>() where T : Component
        => TryGetComponent<T>(out var component) ? component : null;
        public T GetOrAdd<T>() where T : Component
        => TryGetComponent<T>(out var component) ? component : Add<T>();

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
#endif
    }
}


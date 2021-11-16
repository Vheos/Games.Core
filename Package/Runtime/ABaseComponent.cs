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
        // Publics
        public T Add<T>() where T : Component
        => _componentCache.Add<T>();
        public T Get<T>() where T : Component
        => _componentCache.Get<T>();
        public bool Has<T>() where T : Component
        => _componentCache.Has<T>();
        public T GetOrAdd<T>() where T : Component
        {
            if (!Has<T>())
                return Add<T>();
            return Get<T>();
        }
        public Component Add(Type type)
        => _componentCache.Add(type);
        public Component Get(Type type)
        => _componentCache.Get(type);
        public bool Has(Type type)
        => _componentCache.Has(type);
        public Component GetOrAdd(Type type)
        {
            if (!Has(type))
                return Add(type);
            return Get(type);
        }

        public void AddToCache(Component component)
        => _componentCache.AddToCache(component);
        public void AddToCache<T>()
        => _componentCache.AddToCache<T>();
        public void AddToCache(Type type)
        => _componentCache.AddToCache(type);

        // Privates
        private ComponentCache _componentCache;

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _componentCache = this.GetOrAddComponent<ComponentCache>();
        }
#else
        public T Add<T>() where T : Component
        => gameObject.AddComponent<T>();
        public T Get<T>() where T : Component
        => GetComponent<T>();
        public bool Has<T>() where T : Component
        => GetComponent<T>() != null;
        public T GetOrAdd<T>() where T : Component
        {
            if (!TryGetComponent<T>(out var component))
                return Add<T>();
            return component;
        }
        public Component Add(Type type)
        => gameObject.AddComponent(type);
        public Component Get(Type type)
        => GetComponent(type);
        public bool Has(Type type)
        => GetComponent(type) != null;
        public Component GetOrAdd(Type type)
        {
            if (!TryGetComponent(type, out var component))
                return Add(type);
            return component;
        }
#endif
    }
}


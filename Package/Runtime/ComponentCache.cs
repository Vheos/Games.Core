#if CACHED_COMPONENTS
namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    sealed public class ComponentCache : APlayable
    {
        // Publics
        public T Add<T>() where T : Component
        {
            T newComponent = gameObject.AddComponent<T>();
            _cachedComponentsByType[typeof(T)] = newComponent;
            return newComponent;
        }
        public T Get<T>() where T : Component
        => (T)_cachedComponentsByType[typeof(T)];
        public T GetSafe<T>() where T : Component
        {
            if (_cachedComponentsByType.TryGetValue(typeof(T), out var component))
                return (T)component;
            return GetComponent<T>();
        }
        public bool Has<T>() where T : Component
        => _cachedComponentsByType.ContainsKey(typeof(T));
        public Component Add(Type type)
        {
            _cachedComponentsByType[type] = gameObject.AddComponent(type);
            return _cachedComponentsByType[type];
        }
        public Component Get(Type type)
        => _cachedComponentsByType[type];
        public Component GetSafe(Type type)
        {
            if (_cachedComponentsByType.TryGetValue(type, out var component))
                return component;
            return GetComponent(type);
        }
        public bool Has(Type type)
        => _cachedComponentsByType.ContainsKey(type);

        // Privates
        private Dictionary<Type, Component> _cachedComponentsByType;
        internal void AddToCache(Component component)
        => _cachedComponentsByType[component.GetType()] = component;
        internal void AddToCache<T>()
        => AddToCache(typeof(T));
        internal void AddToCache(Type type)
        {
            if (_cachedComponentsByType.ContainsKey(type))
                return;
            if (!TryGetComponent(type, out var component))
            {
                WarningComponentNotFound(type);
                return;
            }
            _cachedComponentsByType.Add(type, component);
        }

        // Warnings
        private void WarningComponentNotFound(Type type)
        => Debug.LogWarning($"{nameof(ComponentCache)} / ComponentNotFound   -   gameobject {name}, component {GetType().Name}, type {type.Name}");

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _cachedComponentsByType = new Dictionary<Type, Component>();
            foreach (var component in GetComponents<Component>())
            {
                if (component is Transform
                || component is ComponentCache)
                    continue;
                AddToCache(component);
            }
        }

#if UNITY_EDITOR
        // Debug
        [ContextMenu(nameof(PrintLocalCache))]
        private void PrintLocalCache()
        {
            int localCachedCount = _cachedComponentsByType.Count;
            int localAllCount = GetComponents<Component>().Length - 1;
            Debug.Log($"{name.ToUpper()} ({localCachedCount}/{localAllCount})");
            foreach (var componentByType in _cachedComponentsByType)
                Debug.Log($"\t- {componentByType.Key.Name}");
            Debug.Log($"");
        }
#endif
    }
}
#endif
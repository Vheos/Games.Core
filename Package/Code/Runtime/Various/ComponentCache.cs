#if CACHED_COMPONENTS
namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;

    sealed internal class ComponentCache : APlayable
    {
        // Internals
        internal T Add<T>() where T : Component
        => (T)(_cachedComponentsByType[typeof(T)] = gameObject.AddComponent<T>());
        internal T Get<T>() where T : Component
        => (T)_cachedComponentsByType[typeof(T)];
        internal bool Has<T>() where T : Component
        => _cachedComponentsByType.ContainsKey(typeof(T));
        internal bool TryGet<T>(out T component) where T : Component
        => _cachedComponentsByType.TryGetAs(typeof(T), out component);
        internal T GetOrAdd<T>() where T : Component
        => _cachedComponentsByType.TryGetAs(typeof(T), out T component) ? component : Add<T>();
        internal void TryAddToCache<T>()
        {
            Type type = typeof(T);
            if (TestForWarnings(type, gameObject))
                return;

            _cachedComponentsByType.Add(type, GetComponent(type));
        }

        // Privates
        private Dictionary<Type, Component> _cachedComponentsByType;
        private void CacheExistingComponents()
        {
            foreach (var component in GetComponents<Component>())
                if (!(component is Transform)
                && !(component is ComponentCache))
                    AddToCache(component);
        }
        private void AddToCache(Component component)
        => _cachedComponentsByType[component.GetType()] = component;

        // Warnings
        private bool TestForWarnings(Type type, GameObject gameObject)
        {
            if (_cachedComponentsByType.ContainsKey(type))
                return WarningComponentAlreadyAdded(type, gameObject);
            if (GetComponent(type) == null)
                return WarningComponentNotFound(type, gameObject);
            return false;
        }
        private bool WarningComponentAlreadyAdded(Type type, GameObject gameObject)
        {
            Debug.LogWarning($"ComponentAlreadyAdded\ttrying to cache an already-cached component {type.Name} in gameobject {gameObject.name}\n" +
            $"Fallback:\treturn without re-caching");
            return true;
        }
        private bool WarningComponentNotFound(Type type, GameObject gameObject)
        {
            Debug.LogWarning($"ComponentNotFound\ttrying to cache component {type.Name}, but it can't be found in gameobject {gameObject.name}\n" +
            $"Fallback:\treturn without caching anything");
            return true;
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _cachedComponentsByType = new Dictionary<Type, Component>();
            CacheExistingComponents();
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



/*
// Publics (Component)
internal Component Add(Type type)
=> _cachedComponentsByType[type] = gameObject.AddComponent(type);
internal Component Get(Type type)
=> _cachedComponentsByType[type];
internal bool Has(Type type)
=> _cachedComponentsByType.ContainsKey(type);
internal bool TryGet(Type type, out Component component)
=> _cachedComponentsByType.TryGet(type, out component);
internal Component GetOrNull(Type type)
=> _cachedComponentsByType.TryGet(type, out var component) ? component : null;
internal Component GetOrAdd(Type type)
=> _cachedComponentsByType.TryGet(type, out var component) ? component : Add(type);

internal void TryAddToCache(Type type)
{
    if (TestForWarnings(type, gameObject))
        return;

    _cachedComponentsByType.Add(type, GetComponent(type));
}
*/
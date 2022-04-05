namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;
    using Tools.Extensions.UnityObjects;
    using Tools.Extensions.General;
    using Tools.Extensions.Collections;

    [DefaultExecutionOrder(-1)]
    abstract public class AStaticManager : ABaseComponent
    {
        // Instance
        abstract private protected void RegisterComponent(ABaseComponent component);
        private protected void RegisterManagerForComponentsOfType<T>() where T : ABaseComponent
        {
            if (!_managersByComponentType.TryAddUnique(typeof(T), this))
                throw new Exception($"Manager for components of type {typeof(T).Name} already exists!");
        }

        // Static
        static private Dictionary<Type, AStaticManager> _managersByComponentType;
        static internal void TryRegisterComponent(ABaseComponent component)
        {
            if (!_managersByComponentType.TryGetValue(component.GetType(), out var manager))
                return;

            manager.RegisterComponent(component);
        }

        // Initializers
        [SuppressMessage("CodeQuality", "IDE0051")]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static private void StaticInitialize()
        => _managersByComponentType = new Dictionary<Type, AStaticManager>();
    }

    abstract public class AStaticManager<TManager, TComponent> : AStaticManager
        where TManager : AStaticManager<TManager, TComponent>
        where TComponent : ABaseComponent
    {
        // Inspector
        [field: SerializeField] public TComponent Prefab { get; private set; }
        [field: SerializeField] public bool PersistentComponents { get; private set; }
        [field: SerializeField] public bool EnsureAnyComponent { get; private set; }

        // Events
        static public AutoEvent<TComponent> OnRegisterComponent;
        static public AutoEvent<TComponent> OnUnregisterComponent;

        // Publics (getters)
        static public TComponent InstantiateComponent(TComponent prefab = null)
        {
            TComponent newComponent;
            if (prefab != null
            || _instance.Prefab.TryNonNull(out prefab))
            {
                newComponent = GameObject.Instantiate<TComponent>(prefab);
                newComponent.name = prefab.name;
            }
            else
            {
                newComponent = new GameObject().AddComponent<TComponent>();
                newComponent.name = typeof(TComponent).Name;
            }

            // Set scene           
            if (_instance.PersistentComponents)
                newComponent.MoveToScene(SceneManager.PersistentScene);

            return newComponent;
        }
        static public IReadOnlyCollection<TComponent> Components
        => _components;
        static public IEnumerable<TComponent> ActiveComponents
        => _components.Where(t => t.isActiveAndEnabled);
        static public TComponent Any
        => _components.FirstOrDefault();
        static public TComponent AnyActive
        => _components.FirstOrDefault(t => t.isActiveAndEnabled);
        static public bool TryGetAny(out TComponent r)
        => Any.TryNonNull(out r);
        static public bool TryGetAnyActive(out TComponent r)
        => AnyActive.TryNonNull(out r);

        // Privates
        static protected TManager _instance;
        static protected HashSet<TComponent> _components;
        override private protected void RegisterComponent(ABaseComponent component)
        {
            TComponent typedComponent = component as TComponent;
            _components.Add(typedComponent);
            OnRegisterComponent.Invoke(typedComponent);
            component.OnPlayDestroy.SubOnce(() => UnregisterComponent(typedComponent));
        }
        private protected void UnregisterComponent(TComponent typedComponent)
        {
            _components.Remove(typedComponent);
            OnUnregisterComponent.Invoke(typedComponent);
        }
        static private void TryCreateFirstComponent()
        {
            if (!_instance.EnsureAnyComponent
            || GameObject.FindObjectOfType<TComponent>(true) != null)
                return;

            InstantiateComponent();
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            OnRegisterComponent = new();
            OnUnregisterComponent = new();
            _instance = this as TManager;
            _components = new();
            RegisterManagerForComponentsOfType<TComponent>();
            TryCreateFirstComponent();
        }

#if UNITY_EDITOR
        // Debug
        // [ContextMenu(nameof(LogComponents))] - generic class methods can't be for menu commands
        public void LogComponents()
        {
            Debug.Log($"{name}.{typeof(TManager).Name}<{typeof(TComponent).Name}> has {_components.Count} components:");
            foreach (var component in _components)
                Debug.Log($"\t- {component.name} (scene: {component.gameObject.scene.name})");
            Debug.Log($"");
        }
#endif
    }
}
namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;
    using Tools.Extensions.General;
    using Tools.Extensions.UnityObjects;
    using Tools.Extensions.Collections;

    [DefaultExecutionOrder(-1)]
    abstract public class AStaticManager : ABaseComponent
    {
        // Instance
        abstract private protected void RegisterComponent(ABaseComponent component);
        private protected void RegisterManagerForComponentsOfType<T>() where T : ABaseComponent
        {
            if (!_managersByComponentType.TryAddUnique(typeof(T), this))
                throw new($"Manager for components of type {typeof(T).Name} already exists!");
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
        => _managersByComponentType = new();
    }

    abstract public class AStaticManager<TManager, TComponent> : AStaticManager
        where TManager : AStaticManager<TManager, TComponent>
        where TComponent : ABaseComponent
    {
        // Inspector
        [field: SerializeField] public TComponent Prefab { get; private set; }
        [field: SerializeField] public bool PersistentComponents { get; private set; }

        // Events
        static public AutoEvent<TComponent> OnRegisterComponent;
        static public AutoEvent<TComponent> OnUnregisterComponent;

        // Publics (getters)
        static public TComponent InstantiateComponent()
        {
            TComponent newComponent;
            if (_instance.Prefab.TryNonNull(out var prefab))
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
        private void UnregisterComponent(TComponent typedComponent)
        {
            _components.Remove(typedComponent);
            OnUnregisterComponent.Invoke(typedComponent);
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            OnRegisterComponent = new();
            OnUnregisterComponent = new();
            _components = new();

            _instance = this as TManager;           
            RegisterManagerForComponentsOfType<TComponent>();
        }
    }
}
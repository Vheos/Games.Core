namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine.SceneManagement;
    using UnityEngine;
    using Tools.Extensions.UnityObjects;
    using Tools.Extensions.General;
    using Tools.Extensions.Collections;

    [DefaultExecutionOrder(-1)]
    abstract public class AComponentManager : ABaseComponent
    {
        // Publics
        static internal bool TryGetComponentManager(Behaviour component, out AComponentManager componentManager)
        => _managersByComponentType.TryGetValue(component.GetType(), out componentManager);
        abstract internal void RegisterComponent(Behaviour component);
        abstract internal void UnregisterComponent(Behaviour component);

        // Privates
        static private Dictionary<Type, AComponentManager> _managersByComponentType;
        private protected void AddManagedComponentType(Type type)
        => _managersByComponentType.Add(type, this);

        // Initializers
        [SuppressMessage("CodeQuality", "IDE0051")]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static private void StaticInitialize()
        => _managersByComponentType = new Dictionary<Type, AComponentManager>();
    }

    abstract public class AComponentManager<TManager, TComponent> : AComponentManager
        where TManager : AComponentManager<TManager, TComponent>
        where TComponent : Behaviour
    {
        // Inspector
        [SerializeField] protected TComponent _Prefab;
        [SerializeField] protected bool _PersistentComponents;
        [SerializeField] protected bool _EnsureNonZeroComponents;

        // Publics (getters)
        static public TComponent Any
        => _components.FirstOrDefault();
        static public TComponent AnyActive
        => _components.FirstOrDefault(t => t.isActiveAndEnabled);
        static public IEnumerable<TComponent> ActiveComponents
        => _components.Where(t => t.isActiveAndEnabled);
        static public bool TryGetAny(out TComponent r)
        => Any.TryNonNull(out r);
        static public bool TryGetAnyActive(out TComponent r)
        => AnyActive.TryNonNull(out r);

        // Publics (adders)
        static public TComponent AddComponentTo(GameObject t)
        {
            TComponent newComponent = t.TryGetComponent(out ABaseComponent baseComponent)
                                    ? baseComponent.Add<TComponent>()
                                    : t.AddComponent<TComponent>();
            if (!_isComponentPlayable)
                RegisterNonPlayableComponent(newComponent);

            return newComponent;
        }
        static public TComponent AddComponentTo(Component t)
        => AddComponentTo(t.gameObject);
        static public TComponent InstantiateComponent()
        {
            TComponent newComponent;
            if (_instance._Prefab.TryNonNull(out var prefab))
            {
                newComponent = GameObject.Instantiate<TComponent>(prefab);
                newComponent.name = prefab.name;
                if (!_isComponentPlayable)
                    RegisterNonPlayableComponent(newComponent);
            }
            else
            {
                newComponent = AddComponentTo(new GameObject());
                newComponent.name = typeof(TComponent).Name;
            }

            // Set scene           
            if (_instance._PersistentComponents)
                newComponent.MoveToScene(SceneManager.PersistentScene);

            return newComponent;
        }

        // Privates  
        static protected TManager _instance;
        static protected HashSet<TComponent> _components;
        static protected bool _isComponentPlayable;
        static private void RegisterNonPlayableComponent(TComponent component)
        {
            _components.Add(component);
            component.GetOrAddComponent<Playable>().OnPlayDestroy.SubscribeOneShot(() => _components.Remove(component));
        }
        static private void InitializeComponentsCollection()
        {
            _components = new HashSet<TComponent>();
            if (!_isComponentPlayable)
            {
                _components.Add(FindObjectsOfType<TComponent>(true));
                foreach (var component in _components)
                    RegisterNonPlayableComponent(component);
            }
        }
        static private void TryCreateFirstComponent(Scene scene)
        {
            if (_components.Any(t => t.gameObject.scene == scene)
            || !_instance._EnsureNonZeroComponents)
                return;

            InstantiateComponent().MoveToScene(scene);
        }
        static private void OnStartLoadingScene(Scene scene)
        {
            if (scene == SceneManager.PersistentScene)
                return;

            InitializeComponentsCollection();
            TryCreateFirstComponent(scene);
        }
        internal override void RegisterComponent(Behaviour component)
        => Debug.Log($"Registering {typeof(TComponent).Name}: {_components.Add(component as TComponent)}");
        internal override void UnregisterComponent(Behaviour component)
        => Debug.Log($"Unregistering {typeof(TComponent).Name}: {_components.Remove(component as TComponent)}");

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _instance = this as TManager;
            _isComponentPlayable = typeof(TComponent).IsAssignableTo<Playable>();
            AddManagedComponentType(typeof(TComponent));

            if (_PersistentComponents)
            {
                InitializeComponentsCollection();
                TryCreateFirstComponent(SceneManager.PersistentScene);
            }
            else
            {
                _components = new HashSet<TComponent>();
                SceneManager.OnStartLoadingScene.SubscribeAuto(this, OnStartLoadingScene);
            }
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
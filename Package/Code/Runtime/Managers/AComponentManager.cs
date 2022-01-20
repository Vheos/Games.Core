namespace Vheos.Games.Core
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
    abstract public class AComponentManager<TManager, TComponent> : AGlobalComponent<TManager>
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
            TComponent newComponent = t.AddComponent<TComponent>();
            RegisterComponent(newComponent);
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
                RegisterComponent(newComponent);
                newComponent.name = prefab.name;
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
        static protected HashSet<TComponent> _components;
        static protected bool _isComponentPlayable;
        static private void RegisterComponent(TComponent component)
        {
            Debug.Log($"Registering {typeof(TComponent).Name}: {_components.Add(component)}");
            var onDestroyEvent = _isComponentPlayable
                ? component.As<Playable>().OnPlayDestroy
                : component.GetOrAddComponent<Playable>().OnPlayDestroy;
            onDestroyEvent.SubscribeOneShot(() => Debug.Log($"Unregistering {typeof(TComponent).Name}: {_components.Remove(component)}"));
        }
        static private void InitializeComponentsCollection()
        {
            _components.Clear();
            foreach (var component in FindObjectsOfType<TComponent>(true))
                RegisterComponent(component);
        }
        static private void TryCreateFirstComponent(Scene scene)
        {
            if (!_instance._EnsureNonZeroComponents
            || _components.Any(t => t.gameObject.scene == scene))
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

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _components = new HashSet<TComponent>();
            _isComponentPlayable = typeof(TComponent).IsAssignableTo<Playable>();

            if (_PersistentComponents)
            {
                InitializeComponentsCollection();
                TryCreateFirstComponent(SceneManager.PersistentScene);
            }
            else
                SceneManager.OnFinishLoadingScene.SubscribeAuto(this, OnStartLoadingScene);
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
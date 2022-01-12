namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.UnityObjects;
    using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

    abstract public class AComponentManager<TManager, TComponent> : AManager<TManager>
        where TManager : AComponentManager<TManager, TComponent>
        where TComponent : Behaviour
    {
        // Inspector
        [SerializeField] protected TComponent _Prefab;
        [SerializeField] protected bool _PersistentComponents;
        [SerializeField] protected bool _EnsureNonZeroComponents;

        // Publics
        static public TComponent FirstActive
        => _components.FirstOrDefault(c => c != null && c.isActiveAndEnabled);
        static public TComponent AddComponentTo(GameObject t)
        {
            TComponent newComponent = t.TryGetComponent(out ABaseComponent baseComponent)
                                    ? baseComponent.Add<TComponent>()
                                    : t.AddComponent<TComponent>();
            _components.Add(newComponent);
            return newComponent;
        }
        static public TComponent InstantiateComponent()
        {
            TComponent newComponent;
            TComponent prefab = _instance._Prefab;
            if (prefab != null)
            {
                newComponent = GameObject.Instantiate<TComponent>(prefab);
                _components.Add(newComponent);
                newComponent.name = prefab.name;
            }
            else
            {
                newComponent = AddComponentTo(new GameObject());
                newComponent.name = typeof(TComponent).Name;
            }

            // Set scene           
            if (_instance._PersistentComponents)
                newComponent.MoveToScene(_instance.gameObject.scene);

            return newComponent;
        }

        // Privates
        static protected HashSet<TComponent> _components;
        static private void RecollectExistingComponents()
        => _components = new HashSet<TComponent>(FindObjectsOfType<TComponent>(true));
        static private void TryCreateFirstComponent(Scene scene)
        {
            if (_components.Any(t => t.gameObject.scene == scene)
            || !_instance._EnsureNonZeroComponents)
                return;

            InstantiateComponent().MoveToScene(scene);
        }
        static private void OnStartLoadingScene(Scene scene)
        {
            if (_instance._PersistentComponents
            || scene == SceneManager.PersistentScene)
                return;

            RecollectExistingComponents();
            TryCreateFirstComponent(scene);
        }

        // Play
        protected override void DefineAutoSubscriptions()
        {
            base.DefineAutoSubscriptions();
            SubscribeAuto(SceneManager.OnStartLoadingScene, OnStartLoadingScene);
        }
        protected override void PlayAwake()
        {
            base.PlayAwake();
            if (!_PersistentComponents)
                return;

            RecollectExistingComponents();
            TryCreateFirstComponent(SceneManager.PersistentScene);
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
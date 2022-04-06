namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
    using Vheos.Tools.Extensions.General;

    [DisallowMultipleComponent]
    public class SceneManager : AStaticComponent<SceneManager>
    {
        // Constants
        private const string PERSISTENT_SCENE_NAME = "Persistent";

        // Inspector
        [field: SerializeField, ScenePicker] public string StartingScene { get; private set; }
        [field: SerializeField] public SceneOperationOrder SceneTransitionOrder { get; private set; }
        [field: SerializeField] public bool WaitInBetween { get; private set; }

        // Events
        static public AutoEvent<Scene> OnStartLoadingScene
        { get; private set; }
        static public AutoEvent<Scene> OnFinishLoadingScene
        { get; private set; }
        static public AutoEvent<Scene> OnStartUnloadingScene
        { get; private set; }
        static public AutoEvent<Scene> OnFinishUnloadingScene
        { get; private set; }
        static public AutoEvent<Scene, Scene> OnChangeActiveScene
        { get; private set; }

        // Publics
        static public bool TransitionTo(string targetScenePath)
        {
            if (targetScenePath == PersistentScene.path)
                return false;

            // Cache
            var transitionOrder = Instance.SceneTransitionOrder;
            var previousActiveScene = ActiveScene;
            Action firstOperationInvoke = transitionOrder.FirstOperation switch
            {
                SceneOperation.Load => () => Load(targetScenePath, transitionOrder.FirstOperationTiming),
                SceneOperation.Unload => () => Unload(previousActiveScene, transitionOrder.FirstOperationTiming),
                _ => default,
            };
            Action secondOperationInvoke = transitionOrder.SecondOperation switch
            {
                SceneOperation.Load => () => Load(targetScenePath, transitionOrder.SecondOperationTiming),
                SceneOperation.Unload => () => Unload(previousActiveScene, transitionOrder.SecondOperationTiming),
                _ => default,
            };

            // Execute
            if (Instance.WaitInBetween)
            {
                AutoEvent<Scene> firstOperationFinishEvent = transitionOrder.FirstOperation switch
                {
                    SceneOperation.Load => OnFinishLoadingScene,
                    SceneOperation.Unload => OnFinishUnloadingScene,
                    _ => default,
                };
                firstOperationFinishEvent.SubEnableDisable(Instance, scene => secondOperationInvoke());
            }

            firstOperationInvoke();
            if (!Instance.WaitInBetween)
                secondOperationInvoke();

            return true;
        }
        static public Scene ActiveScene
        {
            get => UnitySceneManager.GetActiveScene();
            set => UnitySceneManager.SetActiveScene(value);
        }
        static public Scene PersistentScene
        {
            get
            {
                if (!_persistentScene.IsValid())
                    _persistentScene = UnitySceneManager.GetSceneByName(PERSISTENT_SCENE_NAME);
                return _persistentScene;
            }
        }

        // Privates
        static private Scene _persistentScene;
        static private void InvokeOnFinishLoadingScene(Scene scene, LoadSceneMode mode)
        => OnFinishLoadingScene.Invoke(scene);
        static private void InvokeOnFinishUnloadingScene(Scene scene)
        => OnFinishUnloadingScene.Invoke(scene);
        static private void InvokeOnChangeActiveScene(Scene from, Scene to)
        => OnChangeActiveScene.Invoke(from, to);
        static private void Unload(Scene scene, SceneOperationTiming timing)
        {
            if (scene == PersistentScene)
                return;

            OnStartUnloadingScene.Invoke(UnitySceneManager.GetSceneByPath(scene.path));
            switch (timing)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                case SceneOperationTiming.Synchronously: UnitySceneManager.UnloadScene(scene); break;
#pragma warning restore CS0618 // Type or member is obsolete
                case SceneOperationTiming.Asynchronously: UnitySceneManager.UnloadSceneAsync(scene); break;
            }
        }
        static private void Load(string scenePath, SceneOperationTiming timing)
        {
            if (scenePath == PersistentScene.path)
                return;

            switch (timing)
            {
                case SceneOperationTiming.Synchronously: UnitySceneManager.LoadScene(scenePath, LoadSceneMode.Additive); break;
                case SceneOperationTiming.Asynchronously: UnitySceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive); break;
            }
            OnStartLoadingScene.Invoke(UnitySceneManager.GetSceneByPath(scenePath));
        }

        // Initializers
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static private void StaticInitialize()
        {
            OnStartLoadingScene = new();
            OnFinishLoadingScene = new();
            OnStartUnloadingScene = new();
            OnFinishUnloadingScene = new();
            OnChangeActiveScene = new();
        }

        // Play
        protected override void PlayAwake()
        {
            base.PlayAwake();
            OnFinishLoadingScene.SubEnableDisable(this, scene => ActiveScene = scene);

            if (!StartingScene.IsNullOrEmpty()
            && UnitySceneManager.sceneCount < 2)
                TransitionTo(StartingScene);
        }
        protected override void PlayEnable()
        {
            base.PlayEnable();
            UnitySceneManager.sceneLoaded += InvokeOnFinishLoadingScene;
            UnitySceneManager.activeSceneChanged += InvokeOnChangeActiveScene;
            UnitySceneManager.sceneUnloaded += InvokeOnFinishUnloadingScene;
        }
        protected override void PlayDisable()
        {
            base.PlayDisable();
            UnitySceneManager.sceneLoaded -= InvokeOnFinishLoadingScene;
            UnitySceneManager.activeSceneChanged -= InvokeOnChangeActiveScene;
            UnitySceneManager.sceneUnloaded -= InvokeOnFinishUnloadingScene;
        }
    }
}

/* DEBUG
    SubscribeAuto(OnStartUnloadingScene, (scene) => Debug.Log($"{Time.frameCount}\tStartUnloading\t{scene.name}"));
    SubscribeAuto(OnFinishUnloadingScene, (scene) => Debug.Log($"{Time.frameCount}\tFinishUnloading\t{scene.name}"));
    SubscribeAuto(OnStartLoadingScene, (scene) => Debug.Log($"{Time.frameCount}\tStartLoading\t{scene.name}"));
    SubscribeAuto(OnFinishLoadingScene, (scene) => Debug.Log($"{Time.frameCount}\tFinishLoading\t{scene.name}"));
    SubscribeAuto(OnChangeActiveScene, (from, to) => Debug.Log($"{Time.frameCount}\tChangeActive\t{to.name}"));
*/
namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Reflection;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.UtilityN;
    using Tools.Extensions.General;
    [DefaultExecutionOrder(-1)]
    [DisallowMultipleComponent]
    abstract public class AUpdateManager : MonoBehaviour
    {
        // Publics
        static internal void RegisterComponentDelayed(AUpdatable component)
        {
            if (_instance != null)
                _instance.StartCoroutine(Coroutines.AfterCurrentTimestep(() => RegisterComponent(component)));
        }
        static internal void UnregisterComponentDelayed(AUpdatable component)
        {
            if (_instance != null)
                _instance.StartCoroutine(Coroutines.AfterCurrentTimestep(() => UnregisterComponent(component)));
        }

        // Privates
        static private AUpdateManager _instance;
        static private Dictionary<Type, HashSet<Method>> _methodListsByType;
        static private Dictionary<Method, HashSet<AUpdatable>> _callListsByMethod;
        static private void InitializeMethodsDictionary()
        => _methodListsByType = new Dictionary<Type, HashSet<Method>>();
        static private void CacheMethodsDictionary(Assembly assembly)
        {
            foreach (var type in Utility.GetDerivedTypes<AUpdatable>(assembly))
                if (!type.IsAbstract)
                {
                    _methodListsByType[type] = new HashSet<Method>();
                    foreach (var method in Utility.GetEnumValues<Method>())
                        if (IsImplementingMethod(type, method.ToString()))
                            _methodListsByType[type].Add(method);
                }
        }
        static private void InitializeCallsDictionary()
        {
            _callListsByMethod = new Dictionary<Method, HashSet<AUpdatable>>();
            foreach (var method in Utility.GetEnumValues<Method>())
                _callListsByMethod.Add(method, new HashSet<AUpdatable>());
        }
        static private void RegisterComponent(AUpdatable component)
        {
            foreach (var method in _methodListsByType[component.GetType()])
                _callListsByMethod[method].Add(component);
        }
        static private void UnregisterComponent(AUpdatable component)
        {
            foreach (var method in _methodListsByType[component.GetType()])
                _callListsByMethod[method].Remove(component);
        }
        static private bool IsImplementingMethod(Type type, string methodName)
        => type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null, new Type[0], new ParameterModifier[0])
        .TryNonNull(out var method)
        && method.DeclaringType != typeof(AUpdatable)
        && method.IsVirtual;

        // Mono
        private void OnEnable()
        {
            _instance = this;
            InitializeMethodsDictionary();
            CacheMethodsDictionary(Assembly.GetCallingAssembly());
            CacheMethodsDictionary(GetType().Assembly);
            InitializeCallsDictionary();
        }
        private void Update()
        {
            foreach (var component in _callListsByMethod[Method.PlayUpdate])
                component.PlayUpdate();
        }
        private void LateUpdate()
        {
            foreach (var component in _callListsByMethod[Method.PlayUpdateLate])
                component.PlayUpdateLate();
        }
        private void FixedUpdate()
        {
            foreach (var component in _callListsByMethod[Method.PlayUpdateFixed])
                component.PlayUpdateFixed();
        }
        private void OnDisable()
        => _instance = null;

        // Enum
        private enum Method
        {
            PlayUpdate,
            PlayUpdateLate,
            PlayUpdateFixed,
        }

#if UNITY_EDITOR
        // Debug
        [ContextMenu("Display Debug Info")]
        public void DisplayDebugInfo()
        {
            Debug.Log($"METHODS BY TYPE");
            foreach (var methodsListsByTypePair in _methodListsByType)
            {
                Debug.Log($"\t{methodsListsByTypePair.Key.Name}:");
                foreach (var method in methodsListsByTypePair.Value)
                    Debug.Log($"\t\t{method}");
            }
            Debug.Log("");
            Debug.Log($"CALLS BY METHOD");
            foreach (var callListsByMethodPair in _callListsByMethod)
            {
                Debug.Log($"\t{callListsByMethodPair.Key}:");
                foreach (var component in callListsByMethodPair.Value)
                    Debug.Log($"\t\t{(component != null ? component.name : "(destroyed)")}");
            }
            Debug.Log("");
        }
#endif
    }
}
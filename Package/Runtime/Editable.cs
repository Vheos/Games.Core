
namespace Vheos.Tools.UnityCore
{
#if UNITY_EDITOR
    using System;
    using System.Reflection;
    using UnityEngine;
    using UnityEditor.Callbacks;
    using Tools.Extensions.General;

    [ExecuteInEditMode]
    abstract public class AEditable : APlayable
    {
        // Virtuals
        virtual public void EditAwake()
        { }     // create component, open scene, enter edit mode, reload scripts
        virtual public void EditStart()
        { }     // after all Awakes
        virtual public void EditEnable()
        { }    // enable component
        virtual public void EditDisable()
        { }   // disable component
        virtual public void EditDestroy()
        { }   // delete component, close scene, exit edit mode
        virtual public void EditAdd()
        { }       // add component
        virtual public void EditInspect()
        { }   // change anything in this component's inspector
        virtual public void EditUpdate()
        { }    // every frame

        // Wrappers
        [DidReloadScripts]
        static private void DidScriptsReload()
        {
            if (!Application.isPlaying)
                foreach (var editable in FindObjectsOfType<AEditable>())
                    editable.EditAwakeAndRegisterUpdate();
        }
        private void Awake()
        {
            if (!Application.isPlaying)
                EditAwakeAndRegisterUpdate();
            else
                PlayAwake();
        }
        private void Start()
        {
            if (!Application.isPlaying)
                EditStart();
            else
                PlayStart();
        }
        private void OnEnable()
        {
            if (!Application.isPlaying)
                EditEnable();
            else
                PlayEnable();
        }
        private void OnDisable()
        {
            if (!Application.isPlaying)
                EditDisable();
            else
                PlayDisable();
        }
        private void OnDestroy()
        {
            if (!Application.isPlaying)
                EditDestroyAndUnregisterUpdate();
            else
                PlayDestroy();
        }
        private void Reset()
        => EditAdd();

        // Methods
        static private bool IsImplementingMethod(Type type, string methodName)
        => type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null, new Type[0], new ParameterModifier[0])
        .TryNonNull(out var method)
        && method.DeclaringType != typeof(AEditable)
        && method.IsVirtual;
        private void EditAwakeAndRegisterUpdate()
        {
            if (IsImplementingMethod(GetType(), nameof(EditUpdate)))
                Camera.onPreCull += EditUpdateOnCameraPreCull;
            EditAwake();
        }
        private void EditDestroyAndUnregisterUpdate()
        {
            Camera.onPreCull -= EditUpdateOnCameraPreCull;
            EditDestroy();
        }
        private void EditUpdateOnCameraPreCull(Camera camera)
        {
            if (this == null)
            {
                Camera.onPreCull -= EditUpdateOnCameraPreCull;
                return;
            }

            if (isActiveAndEnabled)
                EditUpdate();
        }

        // Debug
        [NonSerialized] public bool EditModeCallbacksDebug;
        [ContextMenu("Toggle EditMode Callbacks Debug")]
        public void ToggleEditModeCallbacksDebug()
        => EditModeCallbacksDebug = !EditModeCallbacksDebug;
    }

#else
    abstract public class AEditable : APlayable
    { }
#endif
}

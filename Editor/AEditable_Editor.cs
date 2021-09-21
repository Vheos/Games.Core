#if UNITY_EDITOR
namespace Vheos.Tools.UnityCore.Editor
{
    using UnityEngine;
    using UnityEditor;
    [CustomEditor(typeof(AEditable), true)]
    [CanEditMultipleObjects]
    public class AEditable_Editor : MonoBehaviour_Editor
    {
        // CONST
        static private readonly GUILayoutOption BUTTON_WIDTH = GUILayout.Width(75f);

        // Editor
        override public void OnInspectorGUI()
        {
            // Draw buttons
            if ((target as AEditable).EditModeCallbacksDebug)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Awake", BUTTON_WIDTH))
                    foreach (AEditable multiTarget in targets)
                        multiTarget.EditAwake();
                if (GUILayout.Button("Start", BUTTON_WIDTH))
                    foreach (AEditable multiTarget in targets)
                        multiTarget.EditStart();

                if (GUILayout.Button("Enable", BUTTON_WIDTH))
                    foreach (AEditable multiTarget in targets)
                        multiTarget.EditEnable();
                if (GUILayout.Button("Disable", BUTTON_WIDTH))
                    foreach (AEditable multiTarget in targets)
                        multiTarget.EditDisable();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Destroy", BUTTON_WIDTH))
                    foreach (AEditable multiTarget in targets)
                        multiTarget.EditDestroy();
                if (GUILayout.Button("Add", BUTTON_WIDTH))
                    foreach (AEditable multiTarget in targets)
                        multiTarget.EditAdd();
                if (GUILayout.Button("Inspect", BUTTON_WIDTH))
                    foreach (AEditable multiTarget in targets)
                        multiTarget.EditInspect();
                if (GUILayout.Button("Update", BUTTON_WIDTH))
                    foreach (AEditable multiTarget in targets)
                        multiTarget.EditUpdate();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            // Draw base inspector
            base.OnInspectorGUI();

            // Inspect
            if (GUI.changed)
                foreach (AEditable multiTarget in targets)
                    multiTarget.EditInspect();
        }
    }
}
#endif
#if UNITY_EDITOR
namespace Vheos.Games.Core.Editor
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(SceneOperationOrder))]
    public class SceneOperationOrder_PropertyDrawer : PropertyDrawer
    {
        private const float LINE_HEIGHT = 18;
        private const int LINES_COUNT = 2;

        private int OppositeOperation(int operation)
        => (SceneOperation)operation switch
        {
            SceneOperation.Load => (int)SceneOperation.Unload,
            SceneOperation.Unload => (int)SceneOperation.Load,
            _ => default,
        };
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return LINE_HEIGHT * LINES_COUNT;
        }

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            // Cache
            Rect controlRect = EditorGUI.PrefixLabel(rect, new GUIContent(" "));
            Rect labelRect = Rect.MinMaxRect(rect.xMin, controlRect.yMin, controlRect.xMin, controlRect.yMax);
            var firstOperation = property.FindPropertyRelative(nameof(SceneOperationOrder.FirstOperation));
            var firstOperationTiming = property.FindPropertyRelative(nameof(SceneOperationOrder.FirstOperationTiming));
            var secondOperation = property.FindPropertyRelative(nameof(SceneOperationOrder.SecondOperation));
            var secondOperationTiming = property.FindPropertyRelative(nameof(SceneOperationOrder.SecondOperationTiming));

            EditorGUI.BeginProperty(rect, label, property);
            {
                // Label
                rect = labelRect;
                EditorGUI.LabelField(rect, label);

                // Controls
                rect = controlRect;
                rect.height = LINE_HEIGHT;

                // First operation
                rect.xMin = controlRect.xMin;
                rect.width = controlRect.width / 3;
                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(rect, firstOperation, GUIContent.none);
                if (EditorGUI.EndChangeCheck())
                    secondOperation.enumValueIndex = OppositeOperation(firstOperation.enumValueIndex);
                rect.xMin += controlRect.width / 3;

                rect.xMax = controlRect.xMax;
                EditorGUI.PropertyField(rect, firstOperationTiming, GUIContent.none);

                // Second operation
                rect.y += LINE_HEIGHT;
                rect.xMin = controlRect.xMin;
                rect.width = controlRect.width / 3;
                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(rect, secondOperation, GUIContent.none);
                if (EditorGUI.EndChangeCheck())
                    firstOperation.enumValueIndex = OppositeOperation(secondOperation.enumValueIndex);
                rect.xMin += controlRect.width / 3;

                rect.xMax = controlRect.xMax;
                EditorGUI.PropertyField(rect, secondOperationTiming, GUIContent.none);

                EditorGUI.EndProperty();
            }

            if(firstOperation.enumValueIndex == secondOperation.enumValueIndex)
                secondOperation.enumValueIndex = OppositeOperation(firstOperation.enumValueIndex);
        }
    }
}
#endif
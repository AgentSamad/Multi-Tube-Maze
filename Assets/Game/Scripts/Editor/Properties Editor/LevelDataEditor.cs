using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    private SerializedProperty levels;

    private void OnEnable()
    {
        levels = serializedObject.FindProperty("Levels");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle boldLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        boldLabelStyle.fontSize = 20;
        boldLabelStyle.alignment = TextAnchor.MiddleCenter;

        boldLabelStyle.clipping = TextClipping.Overflow;

        EditorGUILayout.LabelField("Levels Editor", boldLabelStyle);

        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(levels, true);


        for (int i = 0; i < levels.arraySize; i++)
        {
            SerializedProperty levelProperty = levels.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(levelProperty, new GUIContent("Level " + (i + 1)), true);
        }

        EditorGUILayout.Space();

        // Add a button to remove the last level.
        if (GUILayout.Button("Remove Last Level") && levels.arraySize > 0)
        {
            levels.DeleteArrayElementAtIndex(levels.arraySize - 1);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Add New Level"))
        {
            levels.InsertArrayElementAtIndex(levels.arraySize);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
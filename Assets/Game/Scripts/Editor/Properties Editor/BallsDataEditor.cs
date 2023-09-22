using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BallsData))]
public class BallsDataEditor : Editor
{
    private SerializedProperty properties;
    private SerializedProperty ballRandomColors;

    private void OnEnable()
    {
        properties = serializedObject.FindProperty("properties");
        ballRandomColors = serializedObject.FindProperty("ballRandomColors");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //RB
        GUIStyle boldLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        boldLabelStyle.fontSize = 20;
        boldLabelStyle.clipping = TextClipping.Overflow;
        boldLabelStyle.alignment = TextAnchor.MiddleCenter;


        EditorGUILayout.LabelField("Rigidbody Properties", boldLabelStyle, GUILayout.Height(20));
        GUILayout.Space(10);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(properties.FindPropertyRelative("interpolation"));
        GUILayout.Space(2);
        EditorGUILayout.PropertyField(properties.FindPropertyRelative("constraints"));
        GUILayout.Space(2);

        EditorGUILayout.PropertyField(properties.FindPropertyRelative("collisionDetectionMode"));
        GUILayout.Space(2);

        float dragValue = properties.FindPropertyRelative("drag").floatValue;
        GUILayout.Space(2);

        properties.FindPropertyRelative("drag").floatValue = EditorGUILayout.Slider("Drag", dragValue, 0.0f, 10.0f);
        GUILayout.Space(2);


        EditorGUI.indentLevel--;

        // Separator Line
        GUILayout.Space(5);

        // Colors
        EditorGUILayout.LabelField("Random Balls Colors", boldLabelStyle, GUILayout.Height(20));
        GUILayout.Space(10);

        // Display the list of colors
        for (int i = 0; i < ballRandomColors.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(ballRandomColors.GetArrayElementAtIndex(i), GUIContent.none);
            if (GUILayout.Button("Remove Color", GUILayout.MaxWidth(100)))
            {
                ballRandomColors.DeleteArrayElementAtIndex(i);
                serializedObject.ApplyModifiedProperties();
                return; // Exit to avoid index out of range error
            }

            EditorGUILayout.EndHorizontal();
        }

        // Add Color button
        if (GUILayout.Button("Add Color"))
        {
            ballRandomColors.InsertArrayElementAtIndex(ballRandomColors.arraySize);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
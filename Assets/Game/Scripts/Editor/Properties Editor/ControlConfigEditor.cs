using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ControlConfig))]
public class ControlConfigEditor : Editor
{
    private SerializedProperty _rotationSpeed;
    private SerializedProperty _minimumThresholdInput;

    private void OnEnable()
    {
        _rotationSpeed = serializedObject.FindProperty("rotationSpeed");
        _minimumThresholdInput = serializedObject.FindProperty("minimumThresholdInput");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle boldLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        boldLabelStyle.fontSize = 20;
        boldLabelStyle.clipping = TextClipping.Overflow;
        boldLabelStyle.alignment = TextAnchor.MiddleCenter;


        EditorGUILayout.LabelField("Control Configurations", boldLabelStyle);
        EditorGUILayout.Space(25);

        EditorGUILayout.PropertyField(_rotationSpeed);

        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(_minimumThresholdInput);

        serializedObject.ApplyModifiedProperties();
    }
}
using UnityEditor;
using UnityEngine;

public class GameSettingsMenu : MonoBehaviour
{
    [MenuItem("Game Settings/Levels Data")]
    public static void OpenLevelsSettings()
    {
        string levelData = "Assets/Game/Scriptable Object/Level Data.asset";
        LevelData data = AssetDatabase.LoadAssetAtPath<LevelData>(levelData);

        if (data != null)
        {
            Selection.activeObject = data;
            EditorGUIUtility.PingObject(data);
        }
        else
        {
            Debug.LogError("Level Data not found. Make sure it's in a " + levelData + ".");
        }
    }
    [MenuItem("Game Settings/Control Configurations")]
    public static void OpenControlConfig()
    {
        string configPath = "Assets/Game/Scriptable Object/Control Configuration.asset";
        ControlConfig controlConfig = AssetDatabase.LoadAssetAtPath<ControlConfig>(configPath);

        if (controlConfig != null)
        {
            Selection.activeObject = controlConfig;
            EditorGUIUtility.PingObject(controlConfig);
        }
        else
        {
            Debug.LogError("Control Config Data not found. Make sure it's in a " + configPath + ".");
        }
    }
    
    
    
    
    [MenuItem("Game Settings/Balls Settings")]
    public static void OpenBallsSettings()
    {
        string ballsPath = "Assets/Game/Scriptable Object/Balls Data.asset";
        BallsData ballsData = AssetDatabase.LoadAssetAtPath<BallsData>(ballsPath);

        if (ballsData != null)
        {
            Selection.activeObject = ballsData;
            EditorGUIUtility.PingObject(ballsData);
        }
        else
        {
            Debug.LogError("Balls Data not found. Make sure it's in a " + ballsPath + ".");
        }
    }
    
    
}
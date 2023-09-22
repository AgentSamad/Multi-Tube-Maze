using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Level Data", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public List<Level> Levels;
}


[System.Serializable]
public class Level
{
    [Header("Balls Amount")] [Range(20, 100)] public int levelBalls;
    [Range(5, 60)] public int ballsToWin;
    [Header("SVG Data")] public string svgFileName;
    public Vector3 tubeOffset;
}
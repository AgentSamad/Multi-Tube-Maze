using System;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData levels;

    [Header("Script References")] [SerializeField]
    private TubeGenerator _tubeGenerator;

    private int currentLevel;

    [Header("Test Level")] [SerializeField]
    private bool isTestLevel;

    [Range(0, 2)] [SerializeField] private int testLevelNo;

    private Level currentLevelData;

    private string LevelNumber = "LevelNumber";

    private int destroyedBalls = 0;
    private bool isfailed;
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;

        if (!PlayerPrefs.HasKey(LevelNumber))
        {
            PlayerPrefs.SetInt(LevelNumber, 0);
        }

        currentLevel = PlayerPrefs.GetInt(LevelNumber);
        currentLevel = isTestLevel ? testLevelNo : currentLevel % levels.Levels.Count;
        currentLevelData = levels.Levels[currentLevel];
        GameManager.onWin += OnWin;
    }

    private void Start()
    {
        LoadLevel();
    }

    void OnWin()
    {
        currentLevel++;
        PlayerPrefs.SetInt(LevelNumber, currentLevel);
    }

    void LoadLevel()
    {
        _tubeGenerator.GenerateTube(currentLevelData);
    }

    public int GetLevelTarget()
    {
        return currentLevelData.ballsToWin;
    }

    public void CheckLevelFail()
    {
        destroyedBalls++;

        if (destroyedBalls >= currentLevelData.levelBalls / 2 && !isfailed)
        {
            isfailed = true;
            GameManager.changeGameState(GameState.Lose);
        }
    }
}
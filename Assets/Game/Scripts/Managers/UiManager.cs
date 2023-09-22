using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header("Pannels")] [SerializeField] GameObject mainMenuPannel;
    [SerializeField] GameObject gamplayPanel;
    [SerializeField] GameObject competePanel;
    [SerializeField] GameObject levelFailPanel;
    [Header("Text")] [SerializeField] private TextMeshProUGUI levelNo;

    private void Awake()
    {
        GameManager.onWin += OnLevelComplete;
        GameManager.onGameplay += Gameplay;
        GameManager.onLose += LevelFail;
        GameManager.onMainMenu += MainMenu;
    }

    void MainMenu()
    {
        ActivePanel(home: true);
    }

    void LevelFail()
    {
        ActivePanel(fail: true);
    }

    void Gameplay()
    {
        ActivePanel(gameplay: true);
        levelNo.text = "Level " + (PlayerPrefs.GetInt("LevelNumber") + 1);
    }

    void OnLevelComplete()
    {
        ActivePanel(complete: true);
    }


    void ActivePanel(bool gameplay = false, bool home = false, bool complete = false, bool fail = false)
    {
        gamplayPanel.SetActive(gameplay);
        mainMenuPannel.SetActive(home);
        competePanel.SetActive(complete);
        levelFailPanel.SetActive(fail);
    }

    public void TapToPlay()
    {
        GameManager.changeGameState.Invoke(GameState.Gameplay);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    MainMenu,
    Gameplay,
    Win,
    Lose
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject confetti;
    public static Action<GameState> changeGameState;

    public static event Action onMainMenu, onGameplay, onWin, onLose;

    void Awake()
    {
        changeGameState += ChangeGameState;
        changeGameState?.Invoke(GameState.MainMenu);
    }

    void ChangeGameState(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.MainMenu:
                onMainMenu?.Invoke();
                break;

            case GameState.Gameplay:
                onGameplay?.Invoke();
                GameEvents.InvokeSetControl(true);
                break;

            case GameState.Win:
                confetti.SetActive(true);
                onWin?.Invoke();
                GameEvents.InvokeSetControl(false);
                break;

            case GameState.Lose:
                onLose?.Invoke();
                GameEvents.InvokeSetControl(false);
                break;
        }
    }

    void OnDestroy()
    {
        onWin = null;
        changeGameState = null;
        onLose = null;
        onGameplay = null;
        onMainMenu = null;
    }
}
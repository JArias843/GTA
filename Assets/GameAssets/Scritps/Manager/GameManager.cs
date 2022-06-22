using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public struct SData
{
    public float m_timer;
    public int m_score;
    public int m_levelID;
}

public enum GameState
{
    MainMenu,
    LoadScreen,
    Playing,
    Pause,
    Victory,
    Lose,
    Exit
}

public class GameManager : TemporalSingleton<GameManager>
{
    public Player m_player;
    private SData m_levelData;
    private GameState m_gameState;
    public GameState GameState { get => m_gameState; set => m_gameState = value; }
    public SData LevelData { get => m_levelData; set => m_levelData = value; }

    void Start()
    {
        UpdateGameState(GameState.Playing);
        LevelData = MapLoader.LoadMap(LevelManager.Instance.LevelID - 1);
    }
    private void HandleVictory()
    {
        // Cargar pantalla de victoria
    }

    private void HandleLose()
    {
        // Cargar pantalla de derrota
    }

    private void HandleMainMenu()
    {
        // Cargar pantalla de menu principal
    }

    private void HandleLoadScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void HandlePlaying()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HandleUnpause()
    {
        Time.timeScale = 1;
    }

    private void HandlePause()
    {
        Time.timeScale = 0;
    }
    
    public void HandleExit()
    {
        Application.Quit();
    }

    public void UpdateGameState(GameState _gameState)
    {
        GameState = _gameState;

        switch (GameState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.LoadScreen:
                HandleLoadScreen();
                break;
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.Exit:
                HandleExit();
                break;
            default:
                break;
        }
    }
}

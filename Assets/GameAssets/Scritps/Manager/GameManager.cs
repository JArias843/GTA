using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelData
{
    public float m_time;
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

public class GameManager : PersistentSingleton<GameManager>
{
    public Player m_player;
    public LevelData m_levelData;
    public GameState m_gameState;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.MainMenu);
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

    public void StartGame()
    {
        MapLoader.LoadMap(0);
    }
    
    public void HandleExit()
    {
        Application.Quit();
    }

    public void UpdateGameState(GameState _gameState)
    {
        m_gameState = _gameState;

        switch (m_gameState)
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

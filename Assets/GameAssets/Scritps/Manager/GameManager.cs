using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelData
{
    public int m_minScore;
    public float m_time;
    public string m_levelName;
}

public enum GameState
{
    MainMenu,
    Playing,
    Pause,
    Victory,
    Lose,
    Exit
}

public class GameManager : PersistentSingleton<GameManager>
{
    public Player m_player;
    public LevelData m_currentLevel;
    public GameState m_gameState;

    // Start is called before the first frame update
    void Start()
    {
        MapLoader.LoadMap(0);
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

    private void HandlePlaying()
    {
        Application.Quit();
    }
    
    private void HandlePause()
    {
        Time.timeScale = 0;
    }
    
    private void HandleExit()
    {
        Time.timeScale = 0;
    }

    public void UpdateGameState(GameState _gameState)
    {
        m_gameState = _gameState;

        switch (m_gameState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
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

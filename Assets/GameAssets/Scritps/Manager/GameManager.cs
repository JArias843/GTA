using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public struct SData
{
    public float m_timer;
    public int m_score;
    public int m_levelID;
    public int m_numSmokeBombs;
    public int m_numHnR;
    public int m_numStuns;
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
        LevelData = MapLoader.LoadMap(LevelManager.Instance.LevelID);
        int numAbilities = 0;
        if(LevelData.m_numSmokeBombs != 0)
        {
            m_player.GetComponent<ThrowSmokeBomb>().InitAbility(LevelData.m_numSmokeBombs, numAbilities);
            ++numAbilities;
        }
        if(LevelData.m_numHnR != 0)
        {
            m_player.GetComponent<HitAndRun>().InitAbility(LevelData.m_numHnR, numAbilities);
            ++numAbilities;
        }
        //if(LevelData.m_numSmokeBombs != 0)
        //{
        //    m_player.GetComponent<ThrowSmokeBomb>().InitAbility(LevelData.m_numSmokeBombs, numAbilities);
        //    ++numAbilities;
        //}
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
    public void HandlePlaying()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HandleUnpause()
    {
        MusicManager.Instance.ResumeBackgroundMusic();
        Time.timeScale = 1;
    }

    private void HandlePause()
    {
        MusicManager.Instance.PauseBackgroundMusic();
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

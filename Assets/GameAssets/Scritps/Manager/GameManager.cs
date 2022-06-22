using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    [SerializeField] Player m_player;
    [SerializeField] List<GameObject> m_layers;

    private SData m_levelData;
    private GameState m_gameState;
    public GameState GameState { get => m_gameState; set => m_gameState = value; }
    public SData LevelData { get => m_levelData; set => m_levelData = value; }

    void Start()
    {
        UpdateGameState(GameState.Playing);
        LevelData = MapLoader.LoadMap(LevelManager.Instance.LevelID);
        InputManager.Instance.OnEscapePressedEvent += OnEscapePressed;

        int numAbilities = 0;
        if (LevelData.m_numSmokeBombs != 0)
        {
            m_player.GetComponent<ThrowSmokeBomb>().InitAbility(LevelData.m_numSmokeBombs, numAbilities);
            ++numAbilities;
        }
        if (LevelData.m_numHnR != 0)
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
        SetActiveOneLayer(2);

        /*Active the victory layer*/
        m_layers[2].transform.GetChild(0).gameObject?.SetActive(true);
        m_layers[2].transform.GetChild(1).gameObject?.SetActive(false);
    }

    private void HandleLose()
    {
        // Cargar pantalla de derrota
        SetActiveOneLayer(2);

        /*Active the lose layer*/
        m_layers[2].transform.GetChild(0).gameObject?.SetActive(false);
        m_layers[2].transform.GetChild(1).gameObject?.SetActive(true);
    }

    public void HandleMainMenu()
    {
        // Cargar pantalla de menu principal
        SceneManager.LoadScene(0);
    }

    public void HandlePlaying()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SetActiveOneLayer(0);
    }

    public void HandleUnpause()
    {
        MusicManager.Instance.ResumeBackgroundMusic();
        UpdateGameState(GameState.Playing);
        Time.timeScale = 1;
    }

    private void HandlePause()
    {
        MusicManager.Instance.PauseBackgroundMusic();
        Time.timeScale = 0;

        SetActiveOneLayer(1);
    }

    public void UpdateGameState(GameState _state)
    {
        GameState = _state;

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
            default:
                break;
        }
    }

    private void SetActiveOneLayer(int _index)
    {
        for (int i = 0; i < m_layers.Count; i++)
        {
            if (i == _index)
                m_layers[i].SetActive(true);
            else
                m_layers[i].SetActive(false);
        }
    }
    private void OnEscapePressed()
    {
        UpdateGameState(GameState.Pause);
    }
}
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
    public int m_numDecoys;
}

public enum GameState
{
    Playing,
    Pause,
    Victory,
    Lose,
    Exit
}

public class GameManager : TemporalSingleton<GameManager>
{
    [Header("Player")]
    [SerializeField] public Player m_player;

    [Header("UI")]
    [SerializeField] List<GameObject> m_layers;

    private SData m_levelData;
    private GameState m_gameState;
    private bool m_isFinish = false;
    public GameState GameState { get => m_gameState; set => m_gameState = value; }
    public SData LevelData { get => m_levelData; set => m_levelData = value; }
    public bool IsFinish { get => m_isFinish; set => m_isFinish = value; }


    void Start()
    {
        UpdateGameState(GameState.Playing);
        LevelData = MapLoader.LoadMap(LevelManager.Instance.LevelID);
        InputManager.Instance.OnEscapePressedEvent += OnEscapePressed;

        // Init abilities
        int numAbilities = 0;
        if (LevelData.m_numSmokeBombs != 0)
        {
            m_player.GetComponent<ThrowSmokeBomb>().InitAbility
                (LevelData.m_numSmokeBombs, numAbilities);
            ++numAbilities;
        }
        if (LevelData.m_numHnR != 0)
        {
            m_player.GetComponent<HitAndRun>().InitAbility(LevelData.m_numHnR, numAbilities);
            ++numAbilities;
        }
        if (LevelData.m_numDecoys != 0)
        {
            m_player.GetComponent<DropDecoy>().InitAbility(LevelData.m_numDecoys, numAbilities);
            ++numAbilities;
        }
    }

    private void Update()
    {
        // Level Contdown
        if(!m_isFinish)
        {
            m_levelData.m_timer -= Time.deltaTime;
            GameCC.Instance?.UpdateTimer(m_levelData.m_timer);

            if (m_levelData.m_timer <= 0 && m_player.CoinsStolen >= m_levelData.m_score)
            {
                UpdateGameState(GameState.Victory);
            }

            else if (m_levelData.m_timer <= 0 && m_player.CoinsStolen < m_levelData.m_score)
            {
                UpdateGameState(GameState.Lose);
            }
        }
    }

    private void HandleVictory()
    {
        // Load Victory Screen
        SetActiveOneLayer(2);

        m_layers[2].transform.GetChild(0).gameObject?.SetActive(true);
        m_layers[2].transform.GetChild(1).gameObject?.SetActive(false);

        MusicManager.Instance?.PauseBackgroundMusic();
        MusicManager.Instance?.PlaySound("Victory");

        m_isFinish = true;
        Time.timeScale = 0;
    }

    private void HandleLose()
    {
        // Load Lose Screen
        SetActiveOneLayer(2);

        m_layers[2].transform.GetChild(0).gameObject?.SetActive(false);
        m_layers[2].transform.GetChild(1).gameObject?.SetActive(true);

        MusicManager.Instance?.PauseBackgroundMusic();
        MusicManager.Instance?.PlaySound("Lose");

        m_isFinish = true;
        Time.timeScale = 0;
    }

    public void HandleMainMenu()
    {
        // Load Main Menu
        SceneManager.LoadScene(0);
    }

    public void HandlePlaying()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1;
        SetActiveOneLayer(0);
    }

    public void HandleUnpause()
    {
        MusicManager.Instance?.ResumeBackgroundMusic();
        UpdateGameState(GameState.Playing);
    }

    private void HandlePause()
    {
        MusicManager.Instance?.PauseBackgroundMusic();
        Time.timeScale = 0;
        SetActiveOneLayer(1);
    }

    public void UpdateGameState(GameState _state)
    {
        GameState = _state;

        switch (GameState)
        {
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
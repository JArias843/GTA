using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;
public enum EState { MAIN_MENU, LEVEL_SELECTOR }

public class MenuManager : TemporalSingleton<MenuManager>
{
    private EState m_state;
    [SerializeField] GameObject m_canvasObj;
    [SerializeField] List<GameObject> m_layers;
    [SerializeField] Animator m_canvasAnim;
    [SerializeField] List<Button> m_buttons;
    public EState State { get => m_state; set => m_state = value; }

    // Start is called before the first frame update
    void Start()
    {
        UpdateState(EState.MAIN_MENU);
        LevelManager.Instance.LevelID = -1;

        MusicManager.Instance.MusicVolume = 0.15f;
        MusicManager.Instance.SfxVolume = 0.4f;
        MusicManager.Instance.PlayBackgroundMusic("Main-Theme");


        Time.timeScale = 1;
    }

    #region MAIN MENU
    public void LevelSelector()
    {
        MusicManager.Instance.PlaySound("mouse_click");
        m_canvasAnim.SetTrigger("Open_LevelSelector");
    }
    public void Exit_MainMenu()
    {
        MusicManager.Instance.PlaySound("mouse_click"); 
        Application.Quit();
    }
    #endregion

    #region LEVEL SELECTOR
    public void Exit_LevelSelector()
    {
        MusicManager.Instance.PlaySound("mouse_click");
        m_canvasAnim.SetTrigger("Open_MainMenu");
    }
    public void LoadLevel(int index)
    {
        m_canvasAnim.SetTrigger("Fade_In");

        MusicManager.Instance.PlaySound("level_selector");
        MusicManager.Instance.PlayBackgroundMusic("LoadScreen_Theme");

        for (int i = 0; i < m_buttons.Count; i++)
        {
            m_buttons[i].enabled = false;
        }

        LevelManager.Instance.LevelID = index;
    }
    #endregion

    public void UpdateState(EState _newState)
    {
        State = _newState;

        switch (m_state)
        {
            case EState.MAIN_MENU:
                SetActiveOneLayer(0);
                break;
            case EState.LEVEL_SELECTOR:
                SetActiveOneLayer(1);
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
}

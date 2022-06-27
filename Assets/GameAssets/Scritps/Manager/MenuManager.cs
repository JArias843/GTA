    using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Utils;
public enum EState { MAIN_MENU, LEVEL_SELECTOR }

public class MenuManager : TemporalSingleton<MenuManager>
{
    private EState m_state;
    [SerializeField] GameObject m_canvasObj;
    [SerializeField] List<GameObject> m_layers;
    [SerializeField] Animator m_canvasAnim;
    public EState State { get => m_state; set => m_state = value; }

    // Start is called before the first frame update
    void Start()
    {
        UpdateState(EState.MAIN_MENU);
        LevelManager.Instance.LevelID = -1;
        MusicManager.Instance.MusicVolume = 0.2f;
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
        m_canvasAnim.ResetTrigger("Open_MainMenu");
        m_canvasAnim.ResetTrigger("Fade_In");
        m_canvasAnim.ResetTrigger("Open_LevelSelector");
        m_canvasAnim.SetTrigger("Fade_In");

        MusicManager.Instance.PlaySound("level_selector");
        MusicManager.Instance.PlayBackgroundMusic("LoadScreen_Theme");

        LevelManager.Instance.LevelID = index;
    }
    #endregion

    public void UpdateState(EState _newState)
    {
        State = _newState;

        switch (m_state)
        {
            case EState.MAIN_MENU:
                m_layers[0].SetActive(true);
                m_layers[1].SetActive(false);
                break;
            case EState.LEVEL_SELECTOR:
                m_layers[0].SetActive(false);
                m_layers[1].SetActive(true);
                break;
        }
    }
}

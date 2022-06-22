using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Utils;
public enum EState { MAIN_MENU, LEVEL_SELECTOR }

public class MenuManager : TemporalSingleton<MenuManager>
{
    private EState m_state;
    [SerializeField] GameObject m_canvasObj;
    [SerializeField] List<GameObject> m_layers;

    private Animator m_canvasAnim;
    public EState State { get => m_state; set => m_state = value; }

    // Start is called before the first frame update
    void Start()
    {
        UpdateState(EState.MAIN_MENU);
        m_canvasAnim = m_canvasObj.GetComponent<Animator>() ? m_canvasObj.GetComponent<Animator>() : null;
    }

    #region MAIN MENU
    public void LevelSelector()
    {
        m_canvasAnim.SetTrigger("Open_LevelSelector");
    }
    public void Exit_MainMenu() { Application.Quit(); }
    #endregion

    #region LEVEL SELECTOR
    public void Exit_LevelSelector()
    {
        print("Exit");
        m_canvasAnim.SetTrigger("Open_MainMenu");
    }
    public void LoadLevel(int index)
    {
        m_canvasAnim.SetTrigger("Fade_In");
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

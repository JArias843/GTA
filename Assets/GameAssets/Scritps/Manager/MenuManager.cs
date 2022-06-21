using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MenuManager : TemporalSingleton<MenuManager>
{
    public enum EState 
    { 
        MAIN_MENU, 
        LEVEL_SELECTOR 
    }

    public EState m_state;
    public Canvas m_canvas;

    // Start is called before the first frame update
    void Start()
    {
        m_state = EState.MAIN_MENU;
    }

    void Update()
    {
        
    }
    public void LevelSelector() { m_state = EState.LEVEL_SELECTOR; }
    public void Menu() { m_state = EState.MAIN_MENU; }
    public void Exit() { Application.Quit(); }
}

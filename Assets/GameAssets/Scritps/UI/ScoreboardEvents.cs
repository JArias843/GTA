using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardEvents : MonoBehaviour
{
    private Animator m_anim;
    [SerializeField] private List<Button> m_buttons;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
    public void OnNextLevelButton()
    {
        m_anim.SetTrigger("Next_Level");
        MusicManager.Instance?.PlaySound("mouse_click");

        for (int i = 0; i < m_buttons.Count; i++)
        {
            m_buttons[i].enabled = false;
        }
    }
    public void OnExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        MusicManager.Instance?.PlaySound("mouse_click");
    }

    public void OnRestartLevel()
    {
        Time.timeScale = 1;
        MusicManager.Instance?.ResumeBackgroundMusic();
        MusicManager.Instance?.PlaySound("mouse_click");
        SceneManager.LoadScene(LevelManager.Instance.LevelID + 1);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_Loader");
        LevelManager.Instance.LevelID++;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

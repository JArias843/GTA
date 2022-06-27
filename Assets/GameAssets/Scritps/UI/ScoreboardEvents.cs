using UnityEngine.SceneManagement;
using UnityEngine;

public class ScoreboardEvents : MonoBehaviour
{
    private Animator m_anim;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
    public void OnNextLevelButton()
    {
        m_anim.SetTrigger("Next_Level");
        MusicManager.Instance?.PlaySound("mouse_click");
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
    }
}

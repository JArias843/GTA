using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{
    public void OnEvent_LevelSelector()
    {
        MenuManager.Instance.UpdateState(EState.LEVEL_SELECTOR);
    }
    public void OnEvent_Menu()
    {
        MenuManager.Instance.UpdateState(EState.MAIN_MENU);
    }
    public void OnEvent_LevelLoader()
    {
        SceneManager.LoadScene("Level_Loader");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

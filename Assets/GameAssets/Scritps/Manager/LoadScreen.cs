using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LoadScreen : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] GameObject fillObj;
    [SerializeField] Image fillLoading;
    [SerializeField] GameObject textToContinueObj;

    private AsyncOperation asyncOperation;
    public static LoadScreen instance;
    public static bool isLoading;
    public static bool loadLevel;
    public static float timer;
    private bool reset;

    private void Awake()
    {
        instance = this;
        fillLoading.fillAmount = 0;
    }

    void Update()
    {
        UpdateLoadScreen();
    }

    private IEnumerator LoadLevel()
    {
        isLoading = true;
        fillLoading.fillAmount = 0;
        yield return new WaitForSeconds(0.5f);

        /*Set level*/
        asyncOperation = SceneManager.LoadSceneAsync(GameManager.Instance.
        m_levelData.m_levelID);

        asyncOperation.allowSceneActivation = false;
        fillLoading.fillAmount = asyncOperation.progress;

        while (!asyncOperation.isDone)
        {
            fillLoading.fillAmount = 
            Mathf.Lerp(fillLoading.fillAmount, asyncOperation.progress, Time.deltaTime / 2);

            if (fillLoading.fillAmount >= 0.8)
                break;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);
        fillLoading.fillAmount = 100;
        yield return new WaitForSeconds(0.5f);
        loadLevel = true;
    }

    private void UpdateLoadScreen()
    {
        if (!reset)
        {
            Reset();
            reset = true;
        }

        if (!isLoading)
            StartCoroutine(LoadLevel());

        if (loadLevel)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                textToContinueObj.SetActive(true);
                fillObj.SetActive(false);
            }
        }
    }

    public void OnPressContinue(InputAction.CallbackContext ctx)
    {
        if(textToContinueObj.activeSelf)
        {
            asyncOperation.allowSceneActivation = true;
            GameManager.Instance.UpdateGameState(GameState.Playing);
            Destroy(textToContinueObj);
            Destroy(fillObj);
            reset = false;
        }
    }

    public void Reset()
    {
        fillObj.SetActive(true);
        fillLoading.fillAmount = 0;
        isLoading = false;
        loadLevel = false;
        timer = 0;
    }
}

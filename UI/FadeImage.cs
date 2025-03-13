using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    public CanvasGroup imageCanvasGroup;
    public GameObject menu;
    public float fadeDuration = 1.0f;
    public GameObject reborn;
    public Camera mainC;
    public Camera rebornC;
    public int waitFrames = 750;

    // 用陣列取代 List，讓 Unity 編輯器可以直接管理
    public GameObject[] uiElements;

    private void Start()
    {
        mainC = GameObject.Find("TheMainCamera").GetComponent<Camera>();
    }

    void OnEnable()
    {
        App_GameMusicController.instance.OnEndGame();
        FirebaseManagerLevel.instance.OnEndGame();

        menu.SetActive(false);
        StartCoroutine(FadeInAndOut());
    }

    public IEnumerator FadeInAndOut()
    {
        // 淡入過程
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        // 淡出過程
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            yield return null;
        }

        PlayReborn();
    }

    void ShowMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    void PlayReborn()
    {
        mainC.enabled = false;
        rebornC.enabled = true;
        reborn.SetActive(true);

        StartCoroutine(WaitAndSwitchBack());
    }

    IEnumerator WaitAndSwitchBack()
    {
        // 禁用所有 UI 元素
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(false);
        }

        float waitTime = waitFrames / (float)Application.targetFrameRate;

        if (Application.targetFrameRate <= 0)
        {
            waitTime = waitFrames / 100f;
        }

        yield return new WaitForSeconds(waitTime);

        // 750 幀後操作
        reborn.SetActive(false);
        mainC.enabled = true;
        rebornC.enabled = false;

        // 重新啟用所有 UI 元素
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(true);
        }

        ShowMenu();
    }
}

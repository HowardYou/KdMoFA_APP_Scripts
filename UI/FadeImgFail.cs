using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeImgFail : MonoBehaviour
{
    public CanvasGroup imageCanvasGroup;
    public GameObject menu;   // 這是你的菜單對象
    public float fadeDuration = 1.0f;  // 控制淡入淡出的時間


    void OnEnable()
    {
        App_GameMusicController.instance.OnEndGame();
        FirebaseManagerLevel.instance.OnEndGame();

        menu.SetActive(false);
        // 當物件啟用時觸發淡入效果
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

        ShowMenu();
    }

    void ShowMenu()
    {
        // 啟用菜單
        menu.SetActive(true);
        Time.timeScale = 0;
    }
}

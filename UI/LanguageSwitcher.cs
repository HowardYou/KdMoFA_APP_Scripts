using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    public GameObject[] chineseUI; // 存放中文 UI 的 GameObject
    public GameObject[] englishUI; // 存放英文 UI 的 GameObject
    public bool isChinese = true; // 控制語言

    void Start()
    {
        SwitchLanguage();
    }

    // 調用此方法來切換語言
    public void SwitchLanguage()
    {
        // 切換語言的 UI 顯示
        if (isChinese)
        {
            SetActiveUI(chineseUI, true);
            SetActiveUI(englishUI, false);
        }
        else
        {
            SetActiveUI(chineseUI, false);
            SetActiveUI(englishUI, true);
        }
    }

    // 啟用或禁用 UI 元素
    void SetActiveUI(GameObject[] uiElements, bool isActive)
    {
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(isActive);
        }
    }

    // 在按鈕上呼叫這個方法來手動切換語言
    public void ToggleLanguage()
    {
        isChinese = !isChinese;
        SwitchLanguage();
    }
}

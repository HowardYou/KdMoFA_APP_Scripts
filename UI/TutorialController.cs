using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public GameObject[] tutorialPages;  // 存儲所有教學頁面
    public Button backButton;
    public MenuController menuController;

    private int currentPageIndex = 0;

    void Start()
    {
        backButton.onClick.AddListener(CloseTutorial);
        UpdatePage();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 檢測是否按下了螢幕
        {
            Vector2 touchPosition = Input.mousePosition;

            // 檢查觸摸位置是在螢幕的左半邊還是右半邊
            if (touchPosition.x < Screen.width / 2)
            {
                ShowPreviousPage();
            }
            else if (touchPosition.x >= Screen.width / 2)
            {
                ShowNextPage();
            }
        }
    }

    void UpdatePage()
    {
        for (int i = 0; i < tutorialPages.Length; i++)
        {
            tutorialPages[i].SetActive(i == currentPageIndex);
        }

        backButton.gameObject.SetActive(currentPageIndex == tutorialPages.Length - 1);
    }

    void ShowNextPage()
    {
        if (currentPageIndex < tutorialPages.Length - 1)
        {
            currentPageIndex++;
            UpdatePage();
        }
    }

    void ShowPreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePage();
        }
    }

    void CloseTutorial()
    {
        tutorialCanvas.SetActive(false);
        menuController.CloseTutorial();  // 呼叫MenuController中的CloseTutorial方法
    }
}

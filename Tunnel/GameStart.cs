using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject opening;      // 開場動畫物件
    public GameObject[] Game;       // 遊戲中需要啟用的物件陣列
    public MenuController menu;
    public Camera animationCamera;  // 開場動畫中的攝影機
    public Camera gameCamera;       // 遊戲中的攝影機
    public bool isOpening;

    void Start()
    {       
        // 暫停遊戲並隱藏所有遊戲物件
        DisableAllObjects();
        animationCamera.enabled = true;   // 啟用開場動畫攝影機
        gameCamera.enabled = false;       // 禁用遊戲攝影機
        isOpening = true;
    }

    // 在開場動畫的最後一幀事件中調用此方法
    public void StartGame()
    {
        // 恢復遊戲並顯示所有遊戲物件
        EnableAllObjects();
        animationCamera.enabled = false;  // 禁用開場動畫攝影機
        gameCamera.enabled = true;        // 啟用遊戲攝影機
        opening.SetActive(false);         // 隱藏開場動畫物件
        isOpening = false;
        menu.OpenTutorial();
        menu.isPaused = true;
        Time.timeScale = 0;
    }

    // 啟用所有遊戲物件
    public void EnableAllObjects()
    {
        foreach (GameObject obj in Game)
        {
            obj.SetActive(true);
        }
    }

    // 禁用所有遊戲物件
    public void DisableAllObjects()
    {
        foreach (GameObject obj in Game)
        {
            obj.SetActive(false);
        }
    }
}

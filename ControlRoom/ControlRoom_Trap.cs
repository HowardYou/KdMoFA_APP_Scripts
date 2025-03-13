using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRoom_Trap : MonoBehaviour
{
    public GameObject[] extraTrap;

    // Start 是在遊戲開始時執行
    void Start()
    {

    }

    // Update 是每一幀都會執行
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 假設陣列中的第一個陷阱物件來檢查狀態
            bool isActive = !extraTrap[0].activeSelf;
            ControlTrap(isActive);
        }
    }

    // 用來根據傳入的布林值啟用或停用陷阱
    public void ControlTrap(bool enable)
    {
        foreach (GameObject trap in extraTrap)
        {
            trap.SetActive(enable);
        }
    }
}

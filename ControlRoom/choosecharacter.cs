using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choosecharacter : MonoBehaviour
{
    public GameObject[] charactersA;  // 角色A的陣列
    public GameObject[] charactersB;  // 角色B的陣列
    public bool useCharacterA;        // 用來決定使用哪個角色陣列

    void Awake()
    {
        // 根據 useCharacterA 啟用或禁用對應的角色陣列
        if (useCharacterA)
        {
            ActivateCharacters(charactersA);
            DeactivateCharacters(charactersB);
        }
        else
        {
            ActivateCharacters(charactersB);
            DeactivateCharacters(charactersA);
        }
    }

    // 啟用角色陣列中的所有角色
    void ActivateCharacters(GameObject[] characters)
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(true);
        }
    }

    // 禁用角色陣列中的所有角色
    void DeactivateCharacters(GameObject[] characters)
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public static GameTime instance = null;

    public float timeRemaining = 300; // 倒數時間（以秒為單位）
    public Text countdownText;
    public GameObject successUI; // 過關UI
    public GameObject failureUI; // 失敗UI
    public bool levelCompleted = false; // 是否達成過關條件

    public Image octagonImage;
    private float elapsedTime = 0f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (countdownText == null)
        {
            countdownText = GetComponent<Text>();
        }

        successUI.SetActive(false);
        failureUI.SetActive(false);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Check if a second has passed
        if (elapsedTime >= 1f)
        {
            elapsedTime = 0f;
            octagonImage.transform.Rotate(0f, 0f, 45f);
        }

        if (!levelCompleted)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                countdownText.text = Mathf.Ceil(timeRemaining).ToString();
            }
            else
            {
                ShowFailureUI();
            }
        }

        if (levelCompleted)
        {
            ShowSuccessUI();
        }
    }

    void ShowSuccessUI()
    {
        Debug.Log("Pass");
        successUI.SetActive(true);
    }

    void ShowFailureUI()
    {
        failureUI.SetActive(true);
    }

    // Method to get the countdown message when level is completed
    public string GetCountdownMessage()
    {
        float finalTime = GetFinalTime();
        return "獲得成績: " + Mathf.Ceil(finalTime) + " 秒";
    }

    public float GetFinalTime()
    {
        return 300f - timeRemaining;
    }
}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public Button menuButton = null;
    public Button continueButton = null;
    public Button RestartButton = null;
    public GameObject tutorialCanvas;
    public Text countdownText;
    public LanguageSwitcher languageSwitcher = null;

    public bool isPaused = false;

    private void Awake()
    {
        languageSwitcher.isChinese = PlayerPrefs.GetInt("LanguageID") == 0;
    }

    void Start()
    {
        menuButton.onClick.AddListener(ToggleMenu);
        continueButton.onClick.AddListener(ContinueGame);
        //RestartButton.onClick.AddListener(RestartScene);
        menuPanel.SetActive(false);
        countdownText.gameObject.SetActive(false);
        tutorialCanvas.SetActive(false);
    }

    void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }

    void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    void ToggleMenu()
    {
        isPaused = !isPaused;
        menuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    void ContinueGame()
    {
        StartCoroutine(ResumeGameAfterDelay(3f));
    }

    IEnumerator ResumeGameAfterDelay(float delay)
    {
        menuPanel.SetActive(false);
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.gameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    void RestartScene()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void OnLocaleChanged(Locale newLocale)
    {
        Debug.Log("語言已切換為: " + newLocale.Identifier);
        // 在這裡處理你想要的語言切換後的操作
        if (newLocale.Identifier.Code == "en")
        {
            PlayerPrefs.SetInt("LanguageID", (int)ELanguage.English);
            languageSwitcher.isChinese = false;
        }
        else
        {
            PlayerPrefs.SetInt("LanguageID", (int)ELanguage.Chinese);
            languageSwitcher.isChinese = true;
        }
        languageSwitcher.SwitchLanguage();
    }

    public void OpenTutorial()
    {
        App_GameMusicController.instance.OnStartTutorial();
        tutorialCanvas.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void CloseTutorial()
    {
        App_GameMusicController.instance.OnStartGame();
        FirebaseManagerLevel.instance.OnStartGame();
        StartCoroutine(ResumeGameAfterDelay(3f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class IndexApp : MonoBehaviour
{
    void Start()
    {
        PlayerPrefab_Manager.ResetPlayerPrefab();

        //重置語言
        PlayerPrefs.SetInt("LanguageID", 0);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }

}

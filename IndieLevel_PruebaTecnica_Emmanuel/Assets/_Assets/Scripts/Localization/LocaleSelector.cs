using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool isActive = false;

    private void Start()
    {
        int localeID = PlayerPrefs.GetInt("LocaleKey");
        ChangeLocale(localeID);
    }

    public void ChangeLocale(int localeID)
    {
        if (isActive) return;

        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", localeID );
        isActive = false;
    }

}

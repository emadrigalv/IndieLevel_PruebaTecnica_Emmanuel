using AYellowpaper.SerializedCollections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnGameStart;

    [Header("Dictionary Dependencies")]
    [SerializedDictionary("UI Elements", "TMP Text")]
    public SerializedDictionary<UIElement, TMP_Text> textElementsUI;
    [SerializedDictionary("UI Elements", "Sliders")]
    public SerializedDictionary<UIElement, Slider> slidersUI;

    [Header("Canvas Dependencies")]
    [SerializedDictionary("Screens", "Canvas Group")]
    public SerializedDictionary<Screens, CanvasGroup> screens;

    public enum Screens
    {
        Intro, HUD, 
        StatsMenu, GameOver
    }

    public enum UIElement
    {
        Health, Coins,
        SawCount, SawDamage,
        SawSpeed, RoundCount,
        CoinsHUD, HealthBar,
        TimerBar
    }

    public void UpdateUI(float value, UIElement type)
    {
        if ((int) type > 6)
        {
            slidersUI[type].value = value;
        }
        else
        {
            textElementsUI[type].text = value.ToString();
        }
    }

    public void InitializeCanvas()
    {
        foreach (Screens element in Enum.GetValues(typeof(Screens)))
        {
            HideCanvasGroup(element);
        }
    }

    public void UpdateStatsMenu(float currentHealth, int currentCoins, SawStats sawStats)
    {
        UpdateUI(currentHealth, UIElement.Health);
        UpdateUI(currentCoins, UIElement.Coins);
        UpdateUI(sawStats.sawCount, UIElement.SawCount);
        UpdateUI(sawStats.sawsDamage, UIElement.SawDamage);
        UpdateUI(sawStats.sawsSpeed, UIElement.SawSpeed);
    }

    public void ShowCanvasGroup(Screens type)
    {
        CanvasGroup canvasGroup = screens[type];

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideCanvasGroup(Screens type)
    {
        CanvasGroup canvasGroup = screens[type];

        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void StartGame()
    {
        OnGameStart?.Invoke();
    }
}

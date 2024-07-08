using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializedDictionary("UI Elements", "TMP Text")]
    public SerializedDictionary<UIElement, TMP_Text> textElementsUI;
    [SerializedDictionary("UI Elements", "Sliders")]
    public SerializedDictionary<UIElement, Slider> slidersUI;

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

    public void ShowCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}

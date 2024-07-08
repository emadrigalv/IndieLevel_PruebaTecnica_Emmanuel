using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TMP_Text textHP;
    [SerializeField] private TMP_Text textCoins;
    [SerializeField] private TMP_Text textSawsCount;
    [SerializeField] private TMP_Text textSawsDamage;
    [SerializeField] private TMP_Text textSawsSpeed;

    public void UpdatePlayerStatus(float playerHP, int coins, SawStats sawStats)
    {
        textHP.text = playerHP.ToString();
        textCoins.text = coins.ToString();
        textSawsCount.text = sawStats.sawCount.ToString();
        textSawsDamage.text = sawStats.sawsDamage.ToString();
        textSawsSpeed.text = sawStats.sawsSpeed.ToString();
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

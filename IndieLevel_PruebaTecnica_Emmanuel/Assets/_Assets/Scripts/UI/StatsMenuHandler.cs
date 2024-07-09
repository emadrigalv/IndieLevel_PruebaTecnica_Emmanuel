using UnityEngine;
using UnityEngine.Events;

public class StatsMenuHandler : MonoBehaviour
{ 
    [HideInInspector] public UnityEvent OnAddSawUpgrade;
    [HideInInspector] public UnityEvent OnIncreaseDamageUpgrade;
    [HideInInspector] public UnityEvent OnIncreaseSpeedUpgrade;
    [HideInInspector] public UnityEvent OnHealPlayer;

    [Header("Upgrades Prices")]
    [SerializeField] private int addSawPrice;
    [SerializeField] private int increaseSawDamagePrice;
    [SerializeField] private int increaseSawSpeedPrice;
    [SerializeField] private int healPlayerPrice;

    public void AddSaw()
    {
        if (Coin.coinsCollected >= addSawPrice)
        {
            Coin.coinsCollected -= addSawPrice;
            OnAddSawUpgrade.Invoke();
        }
        else Debug.Log("Not enough coins");
    }

    public void IncreaseDamage()
    {
        if (Coin.coinsCollected >= increaseSawDamagePrice)
        {
            Coin.coinsCollected -= increaseSawDamagePrice;
            OnIncreaseDamageUpgrade.Invoke();
        }
        else Debug.Log("Not enough coins");
    }

    public void IncreaseSpeed()
    {
        if (Coin.coinsCollected >= increaseSawSpeedPrice)
        {
            Coin.coinsCollected -= increaseSawSpeedPrice;
            OnIncreaseSpeedUpgrade.Invoke();
        }
        else Debug.Log("Not enough coins");
    }

    public void HealPlayer()
    {
        if (Coin.coinsCollected >= healPlayerPrice)
        {
            Coin.coinsCollected -= healPlayerPrice;
            OnHealPlayer.Invoke();
        }
        else Debug.Log("Not enough coins");
    }
}

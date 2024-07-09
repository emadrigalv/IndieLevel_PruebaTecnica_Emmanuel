using System.Collections.Generic;
using UnityEngine;

public class SawsHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Player player;
    [SerializeField] private List<Saw> saws;

    [Header("Parameters")]
    [SerializeField] private float speedUpgradePorcentage;
    [SerializeField] private int upgradeDamage;
    [SerializeField] private SawStats initialSawData;

    private float speedMultiplier;
    private SawStats currentSawStats;

    private void Start()
    {
        // Debugging purposes
        InitializeSaws();        
    }

    public void InitializeSaws()
    {
        initialSawData.sawsDamage = player.InitializePlayerDamage();
        initialSawData.sawCount = 0;
        speedMultiplier = 1.0f;

        currentSawStats = initialSawData;

        foreach (Saw saw in saws) 
        {
            saw.InitializeSaw();
            saw.SetSawSpeed(initialSawData.sawsSpeed);
            saw.SetDamage(initialSawData.sawsDamage);
        }

        AddSaw();
    }

    [ContextMenu("More Speed")]
    public void UpgradeSawSpeed()
    {
        speedMultiplier += speedUpgradePorcentage;
        currentSawStats.sawsSpeed = speedMultiplier * initialSawData.sawsSpeed;

        foreach (Saw saw in saws)
        {
            saw.SetSawSpeed(currentSawStats.sawsSpeed);
        }
    }

    [ContextMenu("Increase Saws Damage")]
    public void IncreaseSawsDamage()
    {
        currentSawStats.sawsDamage += upgradeDamage;

        foreach (Saw saw in saws)
        {
            saw.SetDamage(currentSawStats.sawsDamage);
        }
    }

    [ContextMenu("Add saw")]
    public void AddSaw()
    {
        if (currentSawStats.sawCount < 4)
        {
            saws[currentSawStats.sawCount].ActivateSaw();
            currentSawStats.sawCount++;
        }
        else
        {
            Debug.Log("Can't add more saws");
        }
    }

    [ContextMenu("Get saws status")]
    public SawStats GetSawsStats()
    {
        Debug.Log($"speed: {currentSawStats.sawsSpeed}, saws: {currentSawStats.sawCount}, damage: {currentSawStats.sawsDamage}");

        return currentSawStats;
    }
}

[System.Serializable]
public struct SawStats
{
    public float sawsSpeed;
    [HideInInspector] public int sawCount;
    [HideInInspector] public int sawsDamage;
}

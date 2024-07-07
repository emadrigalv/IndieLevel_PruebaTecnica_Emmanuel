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
    [SerializeField] private SawStats sawStats;

    private float speedMultiplier;

    private void Start()
    {
        // Debugging purposes
        InitializeSaws();
        
    }

    public void InitializeSaws()
    {
        sawStats.sawsDamage = player.InitializePlayerDamage();
        speedMultiplier = 1.0f;

        foreach (Saw saw in saws) 
        {
            saw.SetSawSpeed(sawStats.sawsSpeed);
            saw.SetDamage(sawStats.sawsDamage);
        }

        AddSaw();
    }

    [ContextMenu("More Speed")]
    public void UpgradeSawSpeed()
    {
        foreach (Saw saw in saws)
        {
            speedMultiplier += speedUpgradePorcentage;
            float speed = speedMultiplier * sawStats.sawsSpeed;
            saw.SetSawSpeed(speed);
        }
    }

    [ContextMenu("Increase Saws Damage")]
    public void IncreaseSawsDamage()
    {
        foreach (Saw saw in saws)
        {
            sawStats.sawsDamage += upgradeDamage;
            saw.SetDamage(sawStats.sawsDamage);
        }
    }

    [ContextMenu("Add saw")]
    public void AddSaw()
    {
        if (sawStats.sawCount < 4)
        {
            saws[sawStats.sawCount].ActivateSaw();
            sawStats.sawCount++;
        }
        else
        {
            Debug.Log("Can't add more saws");
        }
    }

    [ContextMenu("Get saws status")]
    public SawStats GetSawsStats()
    {
        Debug.Log($"speed: {sawStats.sawsSpeed}, saws: {sawStats.sawCount}, damage: {sawStats.sawsDamage}");

        return sawStats;
    }
}

[System.Serializable]
public struct SawStats
{
    public float sawsSpeed;
    [HideInInspector] public int sawCount;
    [HideInInspector] public int sawsDamage;
}

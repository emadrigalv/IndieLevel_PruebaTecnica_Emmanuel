using System.Collections.Generic;
using UnityEngine;

public class SawsHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private List<SawOrbit> saws = new List<SawOrbit>();

    [Header("Parameters")]
    [SerializeField] private float speedUpgradePorcentage;

    private int sawCounter = 0;

    public float sawsSpeed {  get; private set; }
    public int sawCount { get; private set; }

    private void Start()
    {
        AddSaw();
    }

    [ContextMenu("More Speed")]
    public void UpgradeSawSpeed()
    {
        foreach (SawOrbit saw in saws)
        {
            saw.UpgradeSawSpeed(speedUpgradePorcentage);
        }
    }

    [ContextMenu("Add saw")]
    public void AddSaw()
    {
        if (sawCounter < 4)
        {
            saws[sawCounter].ActivateSaw();
            sawCounter++;
        }
        else
        {
            Debug.Log("Can't add more saws");
        }
    }

    [ContextMenu("Get saws status")]
    public void GetSawsStatus()
    {
        float speed;
        saws[0].GetSawStatus(out speed);

        sawsSpeed = speed;
        sawCount = sawCounter;

        Debug.Log($"speed: {sawsSpeed}, saws: {sawCount}");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPickable, IPooledObject
{
    public static int coinsCollected;

    [Header("Parameters")]
    [SerializeField] private int coinScore;

    public static void InitializeScore()
    {
        coinsCollected = 0;
    }

    public static void CoinCollected(int score)
    {
        coinsCollected += score;
    }

    public void OnObjectSpawn()
    {
        // Play Coin animation
    }

    public void TakeIt()
    {
        CoinCollected(coinScore);
        gameObject.SetActive(false);
    }
}

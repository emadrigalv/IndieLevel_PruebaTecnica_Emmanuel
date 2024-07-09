using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPickable, IPooledObject
{
    public static int coinsCollected;

    [Header("Parameters")]
    [SerializeField] private int coinScore;
    [SerializeField] private Animator animator;

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
        animator.Rebind();
        animator.Update(0);
        StartCoroutine(TimeToDisapear());
    }

    public void TakeIt()
    {
        CoinCollected(coinScore);
        gameObject.SetActive(false);
    }

    private IEnumerator TimeToDisapear()
    {
        Debug.Log("Se inicio el contador");

        yield return new WaitForSeconds(5);

        gameObject.SetActive(false);
        Debug.Log("Se desaparecio");
    }
}

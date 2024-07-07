using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    public void TakeIt()
    {
        Debug.Log("Plata recogida rey");
        gameObject.SetActive(false);
    }
}

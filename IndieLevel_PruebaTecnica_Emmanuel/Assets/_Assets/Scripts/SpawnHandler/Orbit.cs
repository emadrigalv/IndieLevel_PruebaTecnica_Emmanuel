using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform targetTransform;

    [Header("Parameters")]
    [SerializeField] private float speed;

    private void Update()
    {
        transform.RotateAround(targetTransform.position, Vector3.forward, speed * Time.deltaTime);
    }
}

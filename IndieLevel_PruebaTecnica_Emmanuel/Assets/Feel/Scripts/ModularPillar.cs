using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularPillar : MonoBehaviour
{
    private bool shouldMoveToTarget = false;
    private Vector3 playerPosition;
    private Vector3 returnPosition;
    private Vector3 targetPosition;

    public static AnimationCurve distanceCurve;
    public const float LERPMOVEMENTSPEED = 26f;
    private float movementMultiplier = 1;

    private const float yValue = -10f;

    [SerializeField] private bool shouldStay = false;
    private bool destinationReached = false;

    private void Awake()
    {
        targetPosition = transform.position;
        returnPosition = new Vector3(transform.position.x, yValue, transform.position.z);
        transform.position = returnPosition;
        movementMultiplier = (targetPosition.y + Mathf.Abs(yValue)) / Mathf.Abs(yValue);

    }

    void LateUpdate()
    {
        if(shouldMoveToTarget)
        {
            if (shouldStay && destinationReached)
                return;

            Vector3 checkPosition = new Vector3(transform.position.x, playerPosition.y, transform.position.z);
            float distanceBetweenPlayer = Vector3.Distance(checkPosition, playerPosition);

            float yTargetValue = Mathf.Clamp(targetPosition.y, Mathf.NegativeInfinity, (distanceCurve.Evaluate(distanceBetweenPlayer) * targetPosition.y));
            Vector3 positionToMove = new Vector3(transform.position.x, yTargetValue, transform.position.z);

            
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, LERPMOVEMENTSPEED * movementMultiplier * Time.deltaTime);

            if ((targetPosition.y - transform.position.y) < 0.01f)
            {
                destinationReached = true;
            }

            shouldMoveToTarget = false;
        }
        else
        {
            if(!shouldStay)
                transform.position = Vector3.MoveTowards(transform.position, returnPosition, LERPMOVEMENTSPEED * Time.deltaTime);
        }
    }

    public void ActivateMovableState(Vector3 playerPosition)
    {
        shouldMoveToTarget = true;
        this.playerPosition = playerPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundHandler : MonoBehaviour
{
    public UnityEvent<int> OnStartRound;
    public UnityEvent OnFinishedRound;

    [Header("Parameters")]
    [SerializeField] private int roundCount;
    [SerializeField] private int timer;

    private int currentTime;
    private bool inRound;

    private void Start()
    {
        roundCount = 1;
    }

    [ContextMenu("Start Round")]
    public void StartRound()
    {
        inRound = true;
        currentTime = timer;

        OnStartRound.Invoke(roundCount);

        StartCoroutine(timerCoroutine());
    }

    [ContextMenu("Round Finished")]
    public void FinishRound()
    {   
        inRound = false;
        OnFinishedRound.Invoke();
        roundCount++;
    }

    public void RestartGame()
    {
        roundCount = 1;
    }

    private IEnumerator timerCoroutine() 
    {
        while (inRound && currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;

            if (currentTime < 5)
                Debug.Log("Quedan " +  currentTime + " Segundos");
        }

        FinishRound();
    }
}

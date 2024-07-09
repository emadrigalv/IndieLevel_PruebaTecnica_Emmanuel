using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RoundHandler : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent<int> OnStartRound;
    public UnityEvent OnFinishedRound;

    [HideInInspector] public UnityEvent<int> OnTimerChanged;

    [Header("Parameters")]
    [SerializeField] private int timer;

    private int currentTime;
    private bool inRound;
    private Coroutine timerRoutine;

    public int roundCount {  get; private set; }

    private void Start()
    {
        InitializeRoundHandler();
    }

    public void InitializeRoundHandler()
    {
        currentTime = timer;
        roundCount = 1;
    }

    [ContextMenu("Start Round")]
    public void StartRound()
    {
        inRound = true;
        currentTime = timer;

        OnStartRound.Invoke(roundCount);

        timerRoutine = StartCoroutine(timerCoroutine());
    }

    [ContextMenu("Round Finished")]
    public void FinishRound()
    {
        Debug.Log("Se invoko la monda");
        inRound = false;
        OnFinishedRound.Invoke();
        roundCount++;
    }

    public void StopRound()
    {
        inRound = false;
        if (timerRoutine != null) StopCoroutine(timerRoutine);

        timerRoutine = null;
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

            OnTimerChanged.Invoke(currentTime);
        }

        FinishRound();
    }
}

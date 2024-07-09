using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private UIHandler uiHandler;
    [SerializeField] private RoundHandler roundHandler;
    [SerializeField] private SawsHandler sawsHandler;
    [SerializeField] private StatsMenuHandler statsMenu;
    [SerializeField] private SpawnHandler spawnHandler;

    private Player player;
    private IGameState currentState;

    private void Start()
    {
        player = playerObject.GetComponent<Player>();
        uiHandler.InitializeCanvas();
        
        ChangeState(new IntroState(uiHandler));
    }

    private void ChangeState(IGameState newGameState)
    {
        if (currentState != null) currentState.ExitState();

        currentState = newGameState;

        if (currentState != null) currentState.EnterState();
    }

    public void HandleStartGame()
    {
        ChangeState(new InRoundState(roundHandler, uiHandler, playerObject));
    }

    public void HandleRoundFinished()
    {
        ChangeState(new ChangingRoundState(uiHandler, player, sawsHandler, statsMenu));
    }

    public void HandleGameOver()
    {
        ChangeState(new GameOverState(player, sawsHandler, roundHandler, uiHandler, spawnHandler));
    }
}
using UnityEngine;

public class GameOverState : IGameState
{
    private Player player;
    private SawsHandler sawsHandler;
    private RoundHandler roundHandler;
    private UIHandler uiHandler;
    private SpawnHandler spawnHandler;

    public GameOverState(Player player, SawsHandler sawsHandler, RoundHandler roundHandler, UIHandler uiHandler, SpawnHandler spawnHandler)
    {
        this.player = player;
        this.sawsHandler = sawsHandler;
        this.roundHandler = roundHandler;
        this.uiHandler = uiHandler;
        this.spawnHandler = spawnHandler;
    }

    public void EnterState()
    {
        roundHandler?.StopRound();
        spawnHandler?.FinishedRound();
        uiHandler?.ShowCanvasGroup(UIHandler.Screens.GameOver); 
    }

    public void ExitState()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        player.HealPlayer();
        float playerHealth = player.GetPlayerHealthStatus();
        sawsHandler.InitializeSaws();
        roundHandler.InitializeRoundHandler();
        spawnHandler.EnemiesInitialize();
        Coin.InitializeScore();

        player.gameObject.SetActive(true);
        uiHandler.UpdateUI(playerHealth, UIHandler.UIElement.HealthBar);
        uiHandler.UpdateUI(roundHandler.roundCount, UIHandler.UIElement.RoundCount);
        uiHandler.UpdateUI(Coin.coinsCollected, UIHandler.UIElement.CoinsHUD);

        uiHandler.HideCanvasGroup(UIHandler.Screens.GameOver);
    }
}

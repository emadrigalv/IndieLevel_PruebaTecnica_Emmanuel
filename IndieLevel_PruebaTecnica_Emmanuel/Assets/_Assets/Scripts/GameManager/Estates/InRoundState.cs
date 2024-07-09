using UnityEngine;

public class InRoundState : IGameState
{
    private RoundHandler roundHandler;
    private UIHandler uiHandler;
    private GameObject playerObject;

    private ItemCollector itemCollector;
    private Player player;

    public InRoundState(RoundHandler roundHandler, UIHandler uIHandler, GameObject player)
    {
        this.roundHandler = roundHandler;
        this.uiHandler = uIHandler;
        this.playerObject = player;
    }

    public void EnterState()
    {
        itemCollector = playerObject.GetComponent<ItemCollector>();
        player = playerObject.GetComponent<Player>();

        SubscribeEvents();

        uiHandler?.ShowCanvasGroup(UIHandler.Screens.HUD);
        uiHandler?.UpdateUI(roundHandler.roundCount, UIHandler.UIElement.RoundCount);
        roundHandler?.StartRound();
    }

    public void ExitState()
    {
        UnsubscribeEvents();
        uiHandler?.HideCanvasGroup(UIHandler.Screens.HUD);
    }

    private void SubscribeEvents()
    {
        itemCollector.OnCoinCollected.AddListener(UpdateCoinScore);
        player.OnHealthChanged.AddListener(UpdateHealthHUD);
        roundHandler.OnTimerChanged.AddListener(UpdateTimer);
    }

    private void UnsubscribeEvents()
    {
        itemCollector.OnCoinCollected.RemoveListener(UpdateCoinScore);
        player.OnHealthChanged.RemoveListener(UpdateHealthHUD);
        roundHandler.OnTimerChanged.RemoveListener(UpdateTimer);
    }

    private void UpdateHealthHUD(float health)
    {
        uiHandler.UpdateUI(health, UIHandler.UIElement.HealthBar);
    }

    private void UpdateCoinScore()
    {
        uiHandler.UpdateUI(Coin.coinsCollected, UIHandler.UIElement.CoinsHUD);
    }

    private void UpdateTimer(int timer)
    {
        uiHandler.UpdateUI(timer, UIHandler.UIElement.TimerBar);
    }
}

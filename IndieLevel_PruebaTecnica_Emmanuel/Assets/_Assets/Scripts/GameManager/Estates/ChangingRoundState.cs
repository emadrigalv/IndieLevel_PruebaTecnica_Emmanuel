using UnityEngine;

public class ChangingRoundState : IGameState
{
    private UIHandler uiHandler;
    private Player player;
    private SawsHandler sawsHandler;
    private StatsMenuHandler statsMenuHandler;

    private SawStats sawStats;
    
    public ChangingRoundState(UIHandler uIHandler, Player player, SawsHandler sawsHandler, StatsMenuHandler statsMenuHandler)
    {
        this.uiHandler = uIHandler;
        this.player = player;
        this.sawsHandler = sawsHandler;
        this.statsMenuHandler = statsMenuHandler;
    }

    public void EnterState()
    {
        SubscribeEvents();

        if (uiHandler != null)
        {
            UpdateStatsMenuUI();
            uiHandler.ShowCanvasGroup(UIHandler.Screens.StatsMenu);
        }
    }

    public void ExitState()
    {
        UnsubscribeEvents();
        if (uiHandler != null) uiHandler.HideCanvasGroup(UIHandler.Screens.StatsMenu);
    }

    private void SubscribeEvents()
    {
        statsMenuHandler.OnAddSawUpgrade.AddListener(AddSaw);
        statsMenuHandler.OnIncreaseDamageUpgrade.AddListener(IncreaseDamage);
        statsMenuHandler.OnIncreaseSpeedUpgrade.AddListener(IncreaseSpeed);
        statsMenuHandler.OnHealPlayer.AddListener(HealPlayer);
    }

    private void UnsubscribeEvents()
    {
        statsMenuHandler.OnAddSawUpgrade.RemoveListener(AddSaw);
        statsMenuHandler.OnIncreaseDamageUpgrade.RemoveListener(IncreaseDamage);
        statsMenuHandler.OnIncreaseSpeedUpgrade.RemoveListener(IncreaseSpeed);
        statsMenuHandler.OnHealPlayer.RemoveListener(HealPlayer);
    }

    private void AddSaw()
    {
        sawsHandler.AddSaw();
        UpdateStatsMenuUI();
    }

    private void IncreaseDamage()
    {
        sawsHandler.IncreaseSawsDamage();
        UpdateStatsMenuUI();
    }

    private void IncreaseSpeed()
    {
        sawsHandler.UpgradeSawSpeed();
        UpdateStatsMenuUI();
    }

    private void HealPlayer()
    {
        player.HealPlayer();
        UpdateStatsMenuUI();
    }

    private void UpdateStatsMenuUI()
    {
        float playerHealth = player.GetPlayerHealthStatus();
        sawStats = sawsHandler.GetSawsStats();
        uiHandler.UpdateStatsMenu(playerHealth, Coin.coinsCollected, sawStats);
    }
}

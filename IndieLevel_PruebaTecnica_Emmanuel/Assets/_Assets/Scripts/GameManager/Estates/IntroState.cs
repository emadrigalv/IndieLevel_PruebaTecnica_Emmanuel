using UnityEngine;

public class IntroState : IGameState
{
    private UIHandler uiHandler;

    public IntroState(UIHandler uiHandler)
    {
        this.uiHandler = uiHandler;
    }

    public void EnterState()
    {
        if (uiHandler != null) 
        {
            uiHandler.ShowCanvasGroup(UIHandler.Screens.Intro);
        }
    }

    public void ExitState()
    {
        if (uiHandler != null)
        {
            uiHandler.HideCanvasGroup(UIHandler.Screens.Intro);
        }
    }
}

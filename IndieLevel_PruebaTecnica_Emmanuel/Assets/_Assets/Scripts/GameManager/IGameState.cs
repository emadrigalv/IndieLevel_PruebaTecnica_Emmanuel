using UnityEditor.Build.Content;

public interface IGameState 
{
    void EnterState();
    void ExitState();
}

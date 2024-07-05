using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    
    private ICommand moveCommand;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        moveCommand = new MoveCommand(player, direction);
    }

    private void FixedUpdate()
    {
        moveCommand?.Execute();
    }
}

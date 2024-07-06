using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D playerRb;

    public override void Move(Vector2 direction)
    {
        playerRb.velocity = direction * characterData.movementSpeed;
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}

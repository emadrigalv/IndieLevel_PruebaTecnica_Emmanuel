using System.Collections;
using UnityEngine;

public class Player : Character
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D playerRb;

    [Header("Parametes")]
    [SerializeField] private float noDamageCooldown;

    private bool onDamage = false;

    public override void Move(Vector2 direction)
    {
        playerRb.velocity = direction * characterData.movementSpeed;
    }

    protected override void Die()
    {
        // Debugin purposes
        gameObject.SetActive(false);
    }

    public int InitializePlayerDamage()
    {
        return characterData.attackDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !onDamage)
        {
            StartCoroutine(GetHurtRoutine(collision));
        }
    }

    private IEnumerator GetHurtRoutine(Collider2D collision)
    {
        onDamage = true;

        Enemy enemy = collision.GetComponent<Enemy>();
        TakeDamage(enemy.Attack());

        Debug.Log("Huy ese mk me mordio, vida: " + currentHealth);

        yield return new WaitForSeconds(noDamageCooldown);
    }
}

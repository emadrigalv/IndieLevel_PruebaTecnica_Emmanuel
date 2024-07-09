using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
    [Header("Events")]
    public UnityEvent OnPlayerDead;

    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D playerRb;

    [Header("Parametes")]
    [SerializeField] private float noDamageCooldown;

    [HideInInspector] public UnityEvent<float> OnHealthChanged;

    private bool onDamage = false;

    public override void Move(Vector2 direction)
    {
        playerRb.velocity = direction * characterData.movementSpeed;
    }

    protected override void Die()
    {
        // Debugin purposes
        gameObject.SetActive(false);
        OnPlayerDead.Invoke();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnHealthChanged.Invoke(currentHealth);
    }

    public void HealPlayer()
    {
        currentHealth = characterData.maxHealth;
        OnHealthChanged.Invoke(currentHealth);
    }

    public float GetPlayerHealthStatus()
    {
        return currentHealth;
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

    private void OnTriggerStay2D(Collider2D collision)
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

        Debug.Log("Health: " + currentHealth);

        yield return new WaitForSeconds(noDamageCooldown);

        onDamage = false;
    }
}

using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Character_SO characterData;

    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = characterData.maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    public abstract void Move(Vector2 direction);

    protected abstract void Die();
}

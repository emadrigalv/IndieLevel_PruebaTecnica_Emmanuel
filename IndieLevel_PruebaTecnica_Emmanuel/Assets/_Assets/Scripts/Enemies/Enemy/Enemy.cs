using JetBrains.Annotations;
using UnityEngine;

public class Enemy : Character
{
    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer enemyRenderer;

    private Transform target;

    private void OnEnable()
    {
        RandomColor();
    }

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector2 direction = target.position - transform.position;
        Move(direction.normalized);
    }

    public override void Move(Vector2 direction)
    {
        transform.Translate(direction * characterData.movementSpeed * Time.deltaTime);
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Coin drop");
    }

    private void RandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        enemyRenderer.color = randomColor;
    }

    public float Attack()
    {
        return characterData.attackDamage;
    }

    public void InitializeEnemy()
    {
        currentHealth = characterData.maxHealth;
    }
}

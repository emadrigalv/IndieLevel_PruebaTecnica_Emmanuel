using UnityEngine;

public class Enemy : Character, IPooledObject
{
    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer enemyRenderer;

    private Transform target;

    public static SpawnHandler spawnHandler;

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
        spawnHandler.EnemyDead(transform.position, gameObject);
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

    public void OnObjectSpawn()
    {
        InitializeEnemy();
    }
}

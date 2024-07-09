using System.Collections;
using UnityEngine;

public class Enemy : Character, IPooledObject
{
    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer enemyRenderer;

    [Header("Parameters")]
    [SerializeField] private float impulseForce = 2.0f;

    private Transform target;
    private bool inDamage = false;

    public static SpawnHandler spawnHandler;

    private void OnEnable()
    {
        RandomColor();
        inDamage = false;
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
        if(!inDamage) transform.Translate(direction * characterData.movementSpeed * Time.deltaTime);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        if (currentHealth > 0) inDamage = true;
    }

    protected override void Die()
    {
        spawnHandler.EnemyDead(transform.position, gameObject);
    }

    public void DamageBackwardsImpulse(Vector3 impulseDirection)
    {
        if (inDamage && currentHealth > 0) 
        {
            StartCoroutine(OnDamageRoutine(impulseDirection));
        }
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

    private IEnumerator OnDamageRoutine(Vector3 impulseDirection)
    {
        float timer = 1.0f;
        while (timer > 0) 
        {
            transform.Translate(impulseDirection * impulseForce * Time.deltaTime);
            timer -= Time.deltaTime;
            yield return null;
        }
        
        inDamage = false;
    }
}

using UnityEngine;

public class Enemy : Character
{
    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer enemyRenderer;


    [SerializeField] private Transform target;

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
        throw new System.NotImplementedException();
    }

    private void RandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        enemyRenderer.color = randomColor;
    }
}

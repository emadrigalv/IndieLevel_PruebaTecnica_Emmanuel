using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private SpriteRenderer sawRenderer;
    [SerializeField] private CircleCollider2D sawCollider;

    private float speed;
    private int attackDamage;

    private void Awake()
    {
        InitializeSaw();
    }

    private void Update()
    {
        transform.RotateAround(targetTransform.position, Vector3.forward, speed * Time.deltaTime);
    }

    public void InitializeSaw()
    {
        sawRenderer.enabled = false;
        sawCollider.enabled = false;
    }

    public void SetSawSpeed(float speedUpgrade)
    {
        speed = speedUpgrade;
    }

    public void ActivateSaw()
    {
        sawRenderer.enabled = true;
        sawCollider.enabled = true;
    }

    public void SetDamage(int damageUpgrade)
    {
        attackDamage = damageUpgrade;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector3 direction = collision.gameObject.transform.position - targetTransform.position;
            collision.GetComponent<Enemy>().TakeDamage(attackDamage);
            collision.GetComponent<Enemy>().DamageBackwardsImpulse(direction);
        }
    }
}

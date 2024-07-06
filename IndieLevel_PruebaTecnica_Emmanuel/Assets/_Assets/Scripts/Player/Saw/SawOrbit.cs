using UnityEngine;

public class SawOrbit : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private SpriteRenderer sawRenderer;
    [SerializeField] private CircleCollider2D sawCollider;

    [Header("Parameters")]
    [SerializeField] private float speed;

    private float speedMultiplier = 1.0f;

    private void Awake()
    {
        sawRenderer.enabled = false;
        sawCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        transform.RotateAround(targetTransform.position, Vector3.forward, speedMultiplier * speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        speedMultiplier = 1.0f;
    }

    public void UpgradeSawSpeed(float speedUpgrade)
    {
        speedMultiplier += speedUpgrade;
    }

    public void ActivateSaw()
    {
        sawRenderer.enabled = true;
        sawCollider.enabled = true;
    }

    public void GetSawStatus(out float speed)
    {
        speed = this.speed * speedMultiplier;
    }
}

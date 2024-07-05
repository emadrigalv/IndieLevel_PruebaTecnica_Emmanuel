using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Casting config")]
    [SerializeField] private LayerMask pistonsLayerMask;
    [SerializeField] private Vector3 halfBoxExtents;
    [SerializeField] private Transform boxCenterTransform;

    [SerializeField] private CharacterController playerController;

    [SerializeField] private float playerSpeed;

    [SerializeField] private float rotationSmoothTime;

    [SerializeField] private Transform cam;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Animator animator;

    private float groundDitance = 0.4f;
    private bool isGrounded;

    private float rotationSmoothVelocity;

    private float gravity = -9.81f;
    private Vector3 velocity;

    Collider[] collidersFound = null;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        collidersFound = Physics.OverlapBox(boxCenterTransform.position, halfBoxExtents, Quaternion.identity, pistonsLayerMask);

        if (collidersFound.Length > 0)
        {
            foreach (Collider pillar in collidersFound)
            {
                pillar.transform.parent.GetComponent<ModularPillar>().ActivateMovableState(transform.position);
            }
        }

        Movement();
    }

    private void Movement()
    {

        //Check if the player is on the Ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDitance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);

        //Player input movement in X and Z axis
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(xAxis, 0, zAxis).normalized;


        animator.SetFloat("Movement", moveInput.magnitude);

        if (moveInput.magnitude >= 0.1f)
        {
            float LookingAngle = Mathf.Atan2(xAxis, zAxis) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, LookingAngle, ref rotationSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, LookingAngle, 0f) * Vector3.forward;
            playerController.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);

        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCenterTransform.position, 2 * halfBoxExtents);
    }
}

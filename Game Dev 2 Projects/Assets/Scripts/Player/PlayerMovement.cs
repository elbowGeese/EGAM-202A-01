using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private InputAction moveAction, sprintAction, jumpAction;
    private Vector2 moveDir;
    private bool isSprinting = false;
    private bool isGrounded = true;
    private bool isJumping = false;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 50f;
    public float leapForce = 500f;

    public float jumpCooldown = 0.3f;
    private float jumpTimer = 0f;

    public float groundedRayDist = 1f;
    public float groundedSphereRadius = 0.5f;
    public LayerMask groundedLayerMask;

    public bool isPaused = false;

    public AudioSource jumpSFX;
    public float sprintPitch = 1.1f;
    public float normalPitch = 1.0f;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isPaused) { moveDir = Vector2.zero; return; }

        isGrounded = GetIsGrounded();
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if(jumpTimer > jumpCooldown)
            {
                if (isGrounded) { isJumping = false; }
            }
        }

        moveDir = moveAction.ReadValue<Vector2>();
        if (sprintAction.WasPressedThisFrame()) { isSprinting = true; }
        else if(sprintAction.WasReleasedThisFrame()) { isSprinting = false; }

        if (isGrounded && jumpAction.WasPressedThisFrame())
        {
            isJumping = true;
            jumpTimer = 0f;

            if (isSprinting)
            {
                rb.AddForce(new Vector3(moveDir.x, 1f, 0f) * leapForce, ForceMode.Impulse);
                jumpSFX.pitch = sprintPitch;
            }
            else
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                jumpSFX.pitch = normalPitch;
            }

            jumpSFX.Play();
        }
    }

    private void FixedUpdate()
    {
        // move on x
        float speed = walkSpeed;
        // if running button down, change speed to runningSpeed
        if (isSprinting) { speed = sprintSpeed; }
        float targetX = moveDir.x * speed;

        rb.linearVelocity = new Vector3(targetX, rb.linearVelocity.y, rb.linearVelocity.z);
    }

    private bool GetIsGrounded()
    {
        if(Physics.SphereCast(transform.position, groundedSphereRadius, -transform.up, out RaycastHit hit, groundedRayDist))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isGrounded) { Gizmos.color = Color.green; }

        Gizmos.DrawSphere(transform.position + -transform.up * groundedRayDist, groundedSphereRadius);
    }
}

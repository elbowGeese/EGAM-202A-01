using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TankInputs))]
public class TankController : MonoBehaviour
{
    [Header("Movement Properties")]
    public float tankSpeed = 15f;
    public float tankRotationSpeed = 20f;

    [Header("Turret Properties")]
    public Transform turretTransform;
    public float turretLagSpeed = 0.5f;
    private Vector3 finalTurretLookDir;

    [Header("Reticle Properties")]
    public Transform reticleTransform;


    // components
    private Rigidbody rb;
    private TankInputs input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<TankInputs>();
    }

    void FixedUpdate()
    {
        if(rb && input)
        {
            HandleMovement();
            HandleTurret();
            HandleReticle();
        }
    }

    private void HandleMovement()
    {
        // move tank forward and back
        Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * tankSpeed * Time.deltaTime);
        rb.MovePosition(wantedPosition);

        // rotate tank 
        Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * input.RotationInput * Time.deltaTime));
        rb.MoveRotation(wantedRotation);
    }

    private void HandleTurret()
    {
        if (turretTransform)
        {
            Vector3 turretLookDir = input.ReticlePosition - turretTransform.position;
            turretLookDir.y = 0f;

            finalTurretLookDir = Vector3.Lerp(finalTurretLookDir, turretLookDir, Time.deltaTime * turretLagSpeed);
            turretTransform.rotation = Quaternion.LookRotation(finalTurretLookDir);
        }
    }

    private void HandleReticle()
    {
        if (reticleTransform)
        {
            reticleTransform.position = input.ReticlePosition;
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class TankInputs : MonoBehaviour
{
    // input
    public Camera cam;
    private InputAction movementAction;

    // properties
    private Vector3 reticlePosition;
    public Vector3 ReticlePosition {  get { return reticlePosition; } }

    private Vector3 reticleNormal;
    public Vector3 ReticleNormal {  get { return reticleNormal; } }

    private float forwardInput;
    public float ForwardInput {  get { return forwardInput; } }

    private float rotationInput;
    public float RotationInput {  get { return rotationInput; } }

    private void Start()
    {
        movementAction = InputSystem.actions.FindAction("Movement");
    }

    void Update()
    {
        if(cam != null)
        {
            HandleInputs();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(reticlePosition, 0.5f);
    }

    private void HandleInputs()
    {
        Ray screenRay = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        if(Physics.Raycast(screenRay, out hit))
        {
            reticlePosition = hit.point;
            reticleNormal = hit.normal;
        }

        forwardInput = movementAction.ReadValue<Vector2>().y;
        rotationInput = movementAction.ReadValue<Vector2>().x;
    }
}

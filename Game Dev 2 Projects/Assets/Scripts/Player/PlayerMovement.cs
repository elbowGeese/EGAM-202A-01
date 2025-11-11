using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLES
    public float moveSpeed = 5f;
    public float rotateSpeed = 30f;

    public bool isPaused = false;

    // REFERENCES
    private InputAction moveAction;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isPaused) { return; }

        // MOVE
        Vector2 inputDir = moveAction.ReadValue<Vector2>();
        Transform cameraHandle = Camera.main.transform;

        Vector3 worldForward = cameraHandle.forward;
        worldForward.y = 0f;
        worldForward.Normalize();

        Vector3 worldRight = cameraHandle.right;
        worldRight.y = 0f;
        worldRight.Normalize();

        Vector3 moveDir = Vector3.zero;
        moveDir += worldRight * inputDir.x;
        moveDir += worldForward * inputDir.y;

        if (moveDir.magnitude > 1f) { moveDir.Normalize(); }

        navMeshAgent.Move(moveDir * moveSpeed * Time.deltaTime);

        // ROTATE
        Vector3 targetLook = Vector3.Lerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        transform.LookAt(targetLook + transform.position);
    }
}

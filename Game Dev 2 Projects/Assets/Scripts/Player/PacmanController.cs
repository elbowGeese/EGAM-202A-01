using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PacmanController : MonoBehaviour
{
    // VARIABLES
    private enum MovementState { NONE, LEFT, RIGHT, UP, DOWN }
    private MovementState movementState;

    // REFERENCES
    private NavMeshAgent navMeshAgent;
    private InputAction moveAction;

    void Start()
    {
        movementState = MovementState.NONE;

        navMeshAgent = GetComponent<NavMeshAgent>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        ReadInput();
        Move();
    }

    private void ReadInput()
    {
        Vector2 inputDir = moveAction.ReadValue<Vector2>();

        if (inputDir.x > 0f)
        {
            movementState = MovementState.RIGHT;
        }

        if (inputDir.x < 0f)
        {
            movementState = MovementState.LEFT;
        }

        if (inputDir.y > 0f)
        {
            movementState = MovementState.UP;
        }

        if (inputDir.y < 0f)
        {
            movementState = MovementState.DOWN;
        }
    }

    private void Move()
    {
        Vector3 targetPos = transform.position;

        switch (movementState)
        {
            case MovementState.LEFT:
                targetPos.x -= 10f;
                break;
            case MovementState.RIGHT:
                targetPos.x += 10f;
                break;
            case MovementState.UP:
                targetPos.z += 10f;
                break;
            case MovementState.DOWN:
                targetPos.z -= 10f;
                break;
            default:
                Debug.Log("Pacman not moving or in unreadable state.");
                return;
        }

        navMeshAgent.destination = targetPos;
    }
}

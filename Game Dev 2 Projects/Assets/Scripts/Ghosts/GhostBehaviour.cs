using UnityEngine;
using UnityEngine.AI;

public class GhostBehaviour : MonoBehaviour
{
    // VARIABLES
    public enum GhostState { CHASE, SCATTER, RETURN_HOME };
    public GhostState state;

    public Transform[] scatterRoute;
    public int currentScatterDest = 0;

    // REFERENCES
    private NavMeshAgent navMeshAgent;
    private GhostChaseState chase;

    private PowerUpHandler powerUpHandler;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        chase = GetComponent<GhostChaseState>();

        powerUpHandler = FindFirstObjectByType<PowerUpHandler>();
    }

    void Update()
    {
        switch (state)
        {
            case GhostState.CHASE:
                chase.ChaseUpdate();

                if (powerUpHandler.IsPoweredUp)
                {
                    state = GhostState.SCATTER;
                }

                break;
            case GhostState.SCATTER:
                ScatterUpdate();

                if (!powerUpHandler.IsPoweredUp)
                {
                    state = GhostState.CHASE;
                }

                break;
            case GhostState.RETURN_HOME:
                ReturnHomeUpdate();
                break;
            default:
                Debug.Log("Attempted to update unknown ghost state.");
                break;
        }
    }

    private void ScatterUpdate()
    {
        navMeshAgent.destination = scatterRoute[currentScatterDest].position;

        if(Vector3.Distance(transform.position, scatterRoute[currentScatterDest].position) < 1f)
        {
            currentScatterDest++;

            if(currentScatterDest >= scatterRoute.Length)
            {
                currentScatterDest = 0;
            }
        }
    }

    private void ReturnHomeUpdate()
    {

    }
}

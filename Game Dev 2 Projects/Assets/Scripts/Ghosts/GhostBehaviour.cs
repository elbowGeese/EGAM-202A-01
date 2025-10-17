using UnityEngine;
using UnityEngine.AI;

public class GhostBehaviour : MonoBehaviour
{
    // VARIABLES
    public enum GhostState { CHASE, SCATTER, RETURN_HOME };
    public GhostState state;

    public Transform[] scatterRoute;
    public int currentScatterDest = 0;

    public Transform home;
    private bool wasEaten = false;

    public Material chaseMaterial, scatterMaterial, returnHomeMaterial;

    // REFERENCES
    private NavMeshAgent navMeshAgent;
    private GhostChaseState chase;
    private MeshRenderer meshRenderer;

    private PowerUpHandler powerUpHandler;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        chase = GetComponent<GhostChaseState>();
        meshRenderer = GetComponent<MeshRenderer>();
        chaseMaterial = meshRenderer.material;

        powerUpHandler = FindFirstObjectByType<PowerUpHandler>();
    }

    void Update()
    {
        switch (state)
        {
            case GhostState.CHASE:
                if(meshRenderer.material != chaseMaterial) { meshRenderer.material = chaseMaterial; }

                chase.ChaseUpdate();

                if (powerUpHandler.IsPoweredUp && !wasEaten)
                {
                    state = GhostState.SCATTER;
                }

                break;
            case GhostState.SCATTER:
                if (meshRenderer.material != scatterMaterial) { meshRenderer.material = scatterMaterial; }

                ScatterUpdate();

                if (!powerUpHandler.IsPoweredUp)
                {
                    state = GhostState.CHASE;
                }

                break;
            case GhostState.RETURN_HOME:
                if (meshRenderer.material != returnHomeMaterial) { meshRenderer.material = returnHomeMaterial; }

                ReturnHomeUpdate();
                break;
            default:
                Debug.Log("Attempted to update unknown ghost state.");
                break;
        }

        if (!powerUpHandler.IsPoweredUp && wasEaten) { wasEaten = false; }
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
        navMeshAgent.destination = home.position;

        if (Vector3.Distance(transform.position, home.position) < 0.1f)
        {
            wasEaten = true;
            state = GhostState.CHASE;
        }
    }
}

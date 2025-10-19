using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GhostBehaviour : MonoBehaviour
{
    // VARIABLES
    public enum GhostState { CHASE, SCATTER, RETURN_HOME };
    public GhostState state;

    public float releaseDelay = 0f;
    private bool isPaused = true;
    private Coroutine releaseRoutine;
    public Vector3 startPos;

    [Header("Chase")]
    public float chaseSpeed = 5f;

    [Header("Scatter")]
    public float scatterSpeed = 5f;
    public Transform[] scatterRoute;
    public int currentScatterDest = 0;

    [Header("Return Home")]
    public float returningSpeed = 5f;
    public Transform home;

    [Header("Materials")]
    public Material chaseMaterial, scatterMaterial, returnHomeMaterial;

    // REFERENCES
    private NavMeshAgent navMeshAgent;
    private GhostChaseState chase;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        chase = GetComponent<GhostChaseState>();
        meshRenderer = GetComponent<MeshRenderer>();
        chaseMaterial = meshRenderer.material;
    }

    IEnumerator ReleaseGhostRoutine()
    {
        yield return new WaitForSeconds(releaseDelay);
        isPaused = false;
    }

    void Update()
    {
        if (isPaused) { return; }

        switch (state)
        {
            case GhostState.CHASE:
                if(meshRenderer.material != chaseMaterial) { meshRenderer.material = chaseMaterial; }
                if(navMeshAgent.speed != chaseSpeed) { navMeshAgent.speed = chaseSpeed; }

                chase.ChaseUpdate();

                if (gameObject.GetComponent<ClydeChase>())
                {
                    currentScatterDest = gameObject.GetComponent<ClydeChase>().currentScatterDest;
                }

                break;
            case GhostState.SCATTER:
                if (meshRenderer.material != scatterMaterial) { meshRenderer.material = scatterMaterial; }
                if (navMeshAgent.speed != scatterSpeed) { navMeshAgent.speed = scatterSpeed; }

                ScatterUpdate();

                if (gameObject.GetComponent<ClydeChase>())
                {
                    gameObject.GetComponent<ClydeChase>().currentScatterDest = currentScatterDest;
                }

                break;
            case GhostState.RETURN_HOME:
                if (meshRenderer.material != returnHomeMaterial) { meshRenderer.material = returnHomeMaterial; }
                if (navMeshAgent.speed != returningSpeed) { navMeshAgent.speed = returningSpeed; }

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
        navMeshAgent.destination = home.position;

        if (Vector3.Distance(transform.position, home.position) < 0.5f)
        {
            state = GhostState.CHASE;
        }
    }

    public void ResetGhost()
    {
        if (releaseRoutine != null)
        {
            StopCoroutine(releaseRoutine);
        }

        isPaused = true;
        state = GhostState.CHASE;
        navMeshAgent.Warp(startPos);
    }

    public void ReleaseGhost()
    {
        releaseRoutine = StartCoroutine(ReleaseGhostRoutine());
    }
}

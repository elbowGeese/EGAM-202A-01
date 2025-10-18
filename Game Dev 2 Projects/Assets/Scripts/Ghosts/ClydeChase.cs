using UnityEngine;
using UnityEngine.AI;

public class ClydeChase : MonoBehaviour, GhostChaseState
{
    // VARIABLES
    public float followDist = 8f;
    public Transform[] scatterRoute;
    public int currentScatterDest = 0;

    // REFERENCES
    private NavMeshAgent navMeshAgent;
    private Transform pacman;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        pacman = FindFirstObjectByType<PacmanController>().transform;
    }

    public void ChaseUpdate()
    {
        if(Vector3.Distance(transform.position, pacman.position) > followDist)
        {
            navMeshAgent.destination = pacman.position;
        }
        else
        {
            PsudoScatter();
        }
    }

    private void PsudoScatter()
    {
        navMeshAgent.destination = scatterRoute[currentScatterDest].position;

        if (Vector3.Distance(transform.position, scatterRoute[currentScatterDest].position) < 1f)
        {
            currentScatterDest++;

            if (currentScatterDest >= scatterRoute.Length)
            {
                currentScatterDest = 0;
            }
        }
    }
}

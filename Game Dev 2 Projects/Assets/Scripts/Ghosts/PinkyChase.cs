using UnityEngine;
using UnityEngine.AI;

public class PinkyChase : MonoBehaviour, GhostChaseState
{
    private NavMeshAgent navMeshAgent;
    private Transform pacman;

    public float deltaMult = 2f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        pacman = FindFirstObjectByType<PacmanController>().transform;
    }

    public void ChaseUpdate()
    {
        Vector3 pacmanPos = pacman.position;
        Vector3 targetDest = pacmanPos + (pacman.forward * deltaMult);

        navMeshAgent.destination = targetDest;
    }
}

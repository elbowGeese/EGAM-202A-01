using UnityEngine;
using UnityEngine.AI;

public class BlinkyChase : MonoBehaviour, GhostChaseState
{
    private NavMeshAgent navMeshAgent;
    private Transform pacman;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        pacman = FindFirstObjectByType<PacmanController>().transform;
    }

    public void ChaseUpdate()
    {
        navMeshAgent.destination = pacman.position;
    }
}

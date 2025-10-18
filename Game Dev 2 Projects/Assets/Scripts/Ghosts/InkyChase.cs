using UnityEngine;
using UnityEngine.AI;

public class InkyChase : MonoBehaviour, GhostChaseState
{
    private NavMeshAgent navMeshAgent;
    private Transform blinky;

    public float deltaMult = 10f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        blinky = FindFirstObjectByType<BlinkyChase>().transform;
    }

    public void ChaseUpdate()
    {
        Vector3 blinkyPos = blinky.position;
        Vector3 targetDest = blinkyPos + (blinky.forward * deltaMult);

        navMeshAgent.destination = targetDest;
    }
}

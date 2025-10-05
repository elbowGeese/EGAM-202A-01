using UnityEngine;

public class GoToPlayer : MonoBehaviour
{
    private Transform player;
    public float timeToReach = 1f;
    private float timer = 0f;

    public Vector3 startPosition;

    void Start()
    {
        player = FindFirstObjectByType<TankController>().transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        Vector3 targetPosition = player.position;
        targetPosition.y = 1.8f;

        transform.LookAt(targetPosition);
        transform.position = Vector3.Lerp(startPosition, targetPosition, timer / timeToReach);

        if (timer >= timeToReach)
        {
            timer = 0f;
            // poop
            this.enabled = false;
        }
    }
}

using UnityEngine;

public class CreateFollowWaypoints : MonoBehaviour
{
    public Vector2 xRange, yRange, zRange;
    private Vector3 waypoint;
    public float speed = 5f;

    void Start()
    {
        transform.position = CreateRandomWaypoint();
        waypoint = CreateRandomWaypoint();
        transform.LookAt(waypoint);
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, waypoint);

        if(dist < 0.1f)
        {
            BeginNewFollow();
            return;
        }

        float step = speed * Time.deltaTime;
        float t = step / dist;
        t = Mathf.Clamp01(t);

        transform.position = Vector3.Lerp(transform.position, waypoint, t);
    }

    // creates a random vector3 position within the given bounds
    private Vector3 CreateRandomWaypoint()
    {
        float x = Random.Range(xRange.x, xRange.y);
        float y = Random.Range(yRange.x, yRange.y);
        float z = Random.Range(zRange.x, zRange.y);
        return new Vector3(x, y, z);
    }

    public void BeginNewFollow()
    {
        waypoint = CreateRandomWaypoint();
        transform.LookAt(waypoint);
    }
}

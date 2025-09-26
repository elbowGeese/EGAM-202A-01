using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public int amountOfTargets = 10;
    public GameObject[] targetPrefabPool;
    public Vector2 xRange, yRange, zRange;

    void Start()
    {
        for (int i = 0; i < amountOfTargets; i++)
        {
            SpawnRandomTarget();
        }
    }

    private void Update()
    {
        if(IsMissingTarget())
        {
            SpawnRandomTarget();
        }
    }

    private GameObject SpawnRandomTarget()
    {
        GameObject target = Instantiate(targetPrefabPool[Random.Range(0, targetPrefabPool.Length)]);

        float x = Random.Range(xRange.x, xRange.y);
        float y = Random.Range(yRange.x, yRange.y);
        float z = Random.Range(zRange.x, zRange.y);
        target.transform.position = new Vector3(x, y, z);

        return target;
    }

    // checks the number of targets in the scene and compares it to the given amountOfTargets
    // returns true if targets in scene is less than amountOfTargets
    private bool IsMissingTarget()
    {
        Target[] targetsInScene = GameObject.FindObjectsByType<Target>(FindObjectsSortMode.None);

        if (targetsInScene.Length < amountOfTargets)
        {
            return true;
        }

        return false;
    }
}

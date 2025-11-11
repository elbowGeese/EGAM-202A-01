using UnityEngine;

public class HerbSpawner : MonoBehaviour
{
    public Transform spawnTransform;
    public GameObject herbPrefab;
    public bool canSpawn = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!canSpawn) { return; }

        if (IsSpawnEmpty())
        {
            SpawnHerb();
        }
    }

    private bool IsSpawnEmpty()
    {
        return spawnTransform.childCount == 0;
    }

    private void SpawnHerb()
    {
        GameObject herb = Instantiate(herbPrefab, spawnTransform);
        herb.transform.localPosition = Vector3.zero;
        herb.GetComponent<Rigidbody>().isKinematic = true;
    }
}

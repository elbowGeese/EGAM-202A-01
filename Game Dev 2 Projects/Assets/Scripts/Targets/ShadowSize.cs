using UnityEngine;

public class ShadowSize : MonoBehaviour
{
    private Transform target;
    private Vector3 startSize;

    public bool followParent = true;
    [Range(0.1f, 10f)] public float sizeMultiplier = 2f;

    void Start()
    {
        target = transform.parent;
        if (!followParent) { transform.parent = null; }
        startSize = transform.localScale;
    }

    void Update()
    {
        if (target == null) { Destroy(gameObject); }

        float currentDist = GetDist();

        float xSize = (startSize.x / (currentDist + 1)) * sizeMultiplier;
        float zSize = (startSize.z / (currentDist + 1)) * sizeMultiplier;

        Vector3 newSize = new Vector3(xSize, transform.localScale.y, zSize);
        transform.localScale = newSize;
    }

    private float GetDist()
    {
        if (target == null) { Destroy(gameObject); }
        return Vector3.Distance(transform.position, target.position);
    }
}

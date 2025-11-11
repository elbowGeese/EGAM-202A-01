using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;
    public Material[] materials;

    void Start()
    {
        int randIndex = Random.Range(0, materials.Length);
        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.material = materials[randIndex];
        }
    }
}

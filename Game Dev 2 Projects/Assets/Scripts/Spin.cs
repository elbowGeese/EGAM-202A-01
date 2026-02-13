using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 axis = Vector3.one;
    public float speed = 1f;

    void Update()
    {
        transform.Rotate(axis * speed * Time.deltaTime);
    }
}

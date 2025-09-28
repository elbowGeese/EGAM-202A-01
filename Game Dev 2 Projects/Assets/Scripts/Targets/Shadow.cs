using Unity.VisualScripting;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public bool setYEveryFrame = false;
    public bool setRotationEveryFrame = false;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    private void Update()
    {
        if (setYEveryFrame)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }

        if (setRotationEveryFrame)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}

using UnityEngine;

public class StayAboveParent : MonoBehaviour
{
    public float yOffset = 5f;
    void Update()
    {
        if(transform.parent != null)
        {
            transform.position = transform.parent.position + (Vector3.up * yOffset);
        }
    }
}

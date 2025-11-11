using UnityEngine;

public class MatchCameraRotation : MonoBehaviour
{
    void Update()
    {
        if(Camera.main != null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}

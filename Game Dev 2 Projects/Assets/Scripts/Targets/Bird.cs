using UnityEngine;

public class Bird : MonoBehaviour
{
    public void HitBird()
    {
        GetComponent<CreateFollowWaypoints>().enabled = false;
        transform.rotation = Quaternion.Euler(180f, transform.rotation.y, transform.rotation.z);

        FailHandler fh = FindFirstObjectByType<FailHandler>();
        if(fh != null)
        {
            fh.EnterFailState();
        }
    }
}

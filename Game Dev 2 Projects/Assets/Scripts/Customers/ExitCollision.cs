using UnityEngine;

public class ExitCollision : MonoBehaviour
{
    public AudioSource doorAudio;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<Customer>() != null)
        {
            doorAudio.Play();
            Destroy(col.gameObject);
        }
    }
}

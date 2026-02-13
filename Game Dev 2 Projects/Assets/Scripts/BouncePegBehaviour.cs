using UnityEngine;

public class BouncePegBehaviour : MonoBehaviour
{
    public float forceStrength = 5f;
    private ObjectShake ObjectShake;
    public float shakeStrength = 0.2f;
    public float shakeDuration = 0.2f;

    public AudioSource bounceSFX;

    private void Start()
    {
        ObjectShake = GetComponent<ObjectShake>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // get the direction the collision is coming from
        Vector3 forceDir = collision.transform.position - transform.position;
        forceDir = forceDir.normalized;
        forceDir.z = 0f;

        // apply force
        Rigidbody colRB = collision.gameObject.GetComponent<Rigidbody>();
        colRB.AddForce(forceDir *  forceStrength, ForceMode.Impulse);

        // feedback
        StartCoroutine(ObjectShake.ShakeObject(shakeStrength, shakeDuration));
        bounceSFX.Play();
    }
}

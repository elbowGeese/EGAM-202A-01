using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BubbleBullet : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 5f;
    public GameObject popParticle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collider)
    {
        // if collider has target script, do damage to it
        if(collider.GetComponent<Target>())
        {
            collider.GetComponent<Target>().HitTarget();
        }

        // pop
        Pop();
    }

    private void Pop()
    {
        if (popParticle)
        {
            Transform pop = Instantiate(popParticle).transform;
            pop.position = transform.position;
        }

        Destroy(gameObject);
    }

    public void Release(Vector3 releaseDir)
    {
        float size = transform.localScale.x;

        rb.useGravity = true;

        rb.AddForce(releaseDir * speed, ForceMode.Impulse);
        rb.AddForce(Vector3.up * speed * size, ForceMode.Impulse);
    }
}

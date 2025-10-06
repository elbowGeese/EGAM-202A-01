using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BubbleBullet : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 5f;
    public GameObject popParticle;

    public AudioClip[] popSounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collider)
    {
        bool breakStreak = true;

        // if collider has target script, do damage to it
        if(collider.GetComponent<Target>())
        {
            collider.GetComponent<Target>().HitTarget();
            breakStreak = false;
        }

        // pop
        Pop();

        // if collider has a bird script, fell the bird
        if (collider.GetComponent<Bird>())
        {
            collider.GetComponent<Bird>().HitBird();
        }

        if (breakStreak) { GameObject.FindFirstObjectByType<ScoreHandler>().BreakStreak(); }
    }

    private void Pop()
    {
        if (popParticle)
        {
            Transform pop = Instantiate(popParticle).transform;
            pop.position = transform.position;

            AudioSource popAudio = pop.GetComponent<AudioSource>();
            popAudio.clip = popSounds[Random.Range(0, popSounds.Length)];
            popAudio.Play();
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

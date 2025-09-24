using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject confettiParticle;
    private Collider col;
    private Animator anim;

    private void Start()
    {
        col = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    public void HitTarget()
    {
        col.enabled = false;
        anim.SetTrigger("hit");

        if (confettiParticle)
        {
            Transform confetti = Instantiate(confettiParticle).transform;
            confetti.position = transform.position;
        }
    }

    public void DestroyFromAnim()
    {
        Destroy(gameObject);
    }
}

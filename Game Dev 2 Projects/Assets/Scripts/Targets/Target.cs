using UnityEngine;

public class Target : MonoBehaviour
{
    public int points = 1;
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

        ScoreHandler score = FindFirstObjectByType<ScoreHandler>();
        if (score)
        {
            score.AddToScore(points);
        }
    }

    public void DestroyFromAnim()
    {
        Destroy(gameObject);
    }
}

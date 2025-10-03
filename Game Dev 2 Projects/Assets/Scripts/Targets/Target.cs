using UnityEngine;

public class Target : MonoBehaviour
{
    public int points = 1;
    public GameObject confettiParticle;
    public GameObject scoreParticlePrefab;

    private Collider col;
    private Animator anim;

    public AudioSource ping, poof;

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

            Transform scoreParticle = Instantiate(scoreParticlePrefab).transform;
            scoreParticle.position = transform.position;
            scoreParticle.GetComponent<ScoreParticle>().SetScoreText(points);
        }

        if (ping)
        {
            ping.pitch = Random.Range(1f, 1.1f);
            ping.Play();
        }

        if (poof)
        {
            poof.Play();
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

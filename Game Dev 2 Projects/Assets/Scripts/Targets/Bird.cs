using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Animator anim;
    public float minTime, maxTime;
    private float timer;

    private GoToPlayer goToPlayer;
    private CreateFollowWaypoints followWaypoints;

    public float rageTime = 0.2f;
    public ParticleSystem poopParticle;

    private AudioSource birdAudio;
    public AudioClip[] birdSounds;
    public AudioClip squelchSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        goToPlayer = GetComponent<GoToPlayer>();
        goToPlayer.enabled = false;
        followWaypoints = GetComponent<CreateFollowWaypoints>();
        followWaypoints.enabled = true;
        birdAudio = GetComponent<AudioSource>();

        ResetTimer();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0f)
        {
            anim.SetTrigger("flap");
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        timer = Random.Range(minTime, maxTime);
    }

    public void HitBird()
    {
        birdAudio.clip = birdSounds[Random.Range(0,birdSounds.Length)];
        birdAudio.Play();

        StartCoroutine(HitPlayerBack());
    }

    IEnumerator HitPlayerBack()
    {
        followWaypoints.enabled = false;

        yield return new WaitForSeconds(rageTime);

        goToPlayer.enabled = true;
        goToPlayer.startPosition = transform.position;

        yield return new WaitForSeconds(goToPlayer.timeToReach - 0.5f);

        poopParticle.Play();
        birdAudio.clip = squelchSound;
        birdAudio.Play();

        yield return new WaitForSeconds(0.5f);

        FailHandler fh = FindFirstObjectByType<FailHandler>();
        if (fh != null)
        {
            fh.EnterFailState();
        }

        goToPlayer.enabled = false;
        followWaypoints.enabled = true;
        followWaypoints.BeginNewFollow();
    }
}

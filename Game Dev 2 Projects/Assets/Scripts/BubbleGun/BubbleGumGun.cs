using UnityEngine;

[RequireComponent(typeof(BubbleGumGunInputs))]
public class BubbleGumGun : MonoBehaviour
{
    private BubbleGumGunInputs bggInputs;

    private float firingTimer = 0f;
    public AnimationCurve sizeOverTime;

    public GameObject bubblePrefab;
    private GameObject currentBubble;

    public ParticleSystem blowingParticles;
    public AudioSource blowingSound;

    void Start()
    {
        bggInputs = GetComponent<BubbleGumGunInputs>();
    }

    void Update()
    {
        if (bggInputs)
        {
            if (bggInputs.Firing)
            {
                if(currentBubble == null)
                {
                    currentBubble = Instantiate(bubblePrefab, transform);
                    currentBubble.transform.position = transform.position;
                }

                firingTimer += Time.deltaTime;
                GrowBubble();
            }

            if (bggInputs.Fired)
            {
                ReleaseBubble();
                ResetFiringTimer();
            }
        }

        UpdateBlowingPitch();
    }

    private void GrowBubble()
    {
        float currentSize = sizeOverTime.Evaluate(firingTimer);
        if (currentBubble)
        {
            currentBubble.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
            currentBubble.transform.position = transform.position + (transform.forward * (currentSize / 2));
        }

        if (blowingParticles)
        {
            if (!blowingParticles.isPlaying && currentSize < sizeOverTime.keys[sizeOverTime.keys.Length - 1].value)
            {
                blowingParticles.Play();
                blowingSound.Play();
            }
            else if (blowingParticles.isPlaying && currentSize >= sizeOverTime.keys[sizeOverTime.keys.Length - 1].value)
            {
                blowingParticles.Stop();
                blowingSound.Stop();
            }
        }
    }

    private void ReleaseBubble()
    {
        if (currentBubble)
        {
            currentBubble.transform.parent = null;
            currentBubble.GetComponent<BubbleBullet>().Release(transform.forward);
            currentBubble = null;
        }

        if (blowingParticles)
        {
            if (blowingParticles.isPlaying)
            {
                blowingParticles.Stop();
                blowingSound.Stop();
            }
        }
    }

    private void ResetFiringTimer()
    {
        firingTimer = 0f;
    }

    private void UpdateBlowingPitch()
    {
        if (blowingSound.isPlaying)
        {
            blowingSound.pitch = 1f + (sizeOverTime.Evaluate(firingTimer) / 2);
        }
        else if(!blowingSound.isPlaying && blowingSound.pitch != 1f)
        {
            blowingSound.pitch = 1f;
        }
    }
}

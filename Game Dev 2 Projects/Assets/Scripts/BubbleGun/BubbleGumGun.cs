using UnityEngine;

[RequireComponent(typeof(BubbleGumGunInputs))]
public class BubbleGumGun : MonoBehaviour
{
    private BubbleGumGunInputs bggInputs;

    private float firingTimer = 0f;
    public AnimationCurve sizeOverTime;

    public GameObject bubblePrefab;
    private GameObject currentBubble;

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
    }

    private void GrowBubble()
    {
        float currentSize = sizeOverTime.Evaluate(firingTimer);
        if (currentBubble)
        {
            currentBubble.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
            currentBubble.transform.position = transform.position + (transform.forward * (currentSize / 2));
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
    }

    private void ResetFiringTimer()
    {
        firingTimer = 0f;
    }
}

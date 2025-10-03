using UnityEngine;

public class Bird : MonoBehaviour
{
    private Animator anim;
    public float minTime, maxTime;
    private float timer;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        GetComponent<CreateFollowWaypoints>().enabled = false;
        transform.rotation = Quaternion.Euler(180f, transform.rotation.y, transform.rotation.z);

        FailHandler fh = FindFirstObjectByType<FailHandler>();
        if(fh != null)
        {
            fh.EnterFailState();
        }
    }
}

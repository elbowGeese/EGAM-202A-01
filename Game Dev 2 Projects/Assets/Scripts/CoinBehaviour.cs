using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private CinemachineImpulseSource impulseSource;
    public float hitForce = 500f;
    public float flipForce = 100f;
    public int numFlips {  get; private set; }
    public float hitStopDelay = 0.1f;
    public GameObject hitParticle;

    private bool isPaused = false;

    public AudioSource sfx_coin;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        numFlips = 0;
    }

    public void PauseCoin(bool pause)
    {
        isPaused = pause;

        rb.useGravity = !pause;
        if (pause)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            StopAllCoroutines();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPaused) { return; }

        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            StartCoroutine(FlipRoutine(collision.transform.position));
        }
    }

    public bool IsCoinFacingUp()
    {
        float facingDir = Vector3.Dot(transform.up, Vector3.up);
        return facingDir < 0f;
    }

    IEnumerator FlipRoutine(Vector3 playerPos)
    {
        numFlips++;

        // particle
        Vector3 midPoint = Vector3.Lerp(transform.position, playerPos, 0.5f);
        Instantiate(hitParticle, midPoint, Quaternion.identity);

        sfx_coin.Play();

        // hitstop
        yield return HitStopRoutine();

        // find which side is up
        //float facingDir = Vector3.Dot(transform.up, Vector3.up);

        rb.AddForce(Vector3.up * hitForce);
        // add force opposite player direction
        Vector3 directionToPlayer = playerPos - transform.position;
        //float playerDot = Vector3.Dot(Vector3.right, directionToPlayer);
        float xDir = directionToPlayer.x / Mathf.Abs(directionToPlayer.x);
        rb.AddForce(Vector3.right * -xDir * (hitForce/2));
        // add torque
        float xyDir = xDir * directionToPlayer.y / Mathf.Abs(directionToPlayer.y);
        rb.AddTorque(Vector3.forward * -xyDir * flipForce);

        //// wait until opposite side is up
        //if (facingDir < 0) // if facing direction is negative, wait for positive
        //{
        //    while(facingDir < 0.95f)
        //    {
        //        facingDir = Vector3.Dot(transform.up, Vector3.up);
        //        yield return null;
        //    }
        //}
        //else // if facing direction is positive, wait for negative
        //{
        //    while (facingDir > -0.95f)
        //    {
        //        facingDir = Vector3.Dot(transform.up, Vector3.up);
        //        yield return null;
        //    }
        //}

        //// stop torque
        //rb.angularVelocity = Vector3.zero;
    }

    IEnumerator HitStopRoutine()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitStopDelay);
        Time.timeScale = 1f;
        impulseSource.GenerateImpulse();
    }
}

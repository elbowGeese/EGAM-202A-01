using UnityEngine;
using System.Collections;

public class CharacterData : MonoBehaviour
{
    [Header("Movement")]
    public Transform characterTransform;
    public Vector3 startPos;
    public float moveSpeed;
    public float width;
    [Header("Jumping")]
    public float height;
    public float maxJumpHeight;
    public AnimationCurve jumpSpeed;
    public AnimationCurve fallSpeed;
    public float jumpDelay;
    public float attackWindowPercent;
    [Header("Feedback")]
    public Animator anim;
    public GameObject successAtkParticle;
    public GameObject failAtkParticle;
    public GameObject dmgPopup;
    public float shakeDuration = 0.05f;
    public int shakeFrequency = 5;

    public void Shake(float shakeStrength)
    {
        StartCoroutine(ShakeRoutine(shakeStrength));
    }

    IEnumerator ShakeRoutine(float shakeStrength)
    {
        Vector3 currentPos = transform.position;

        for (int i = 0; i < shakeFrequency; i++)
        {
            transform.position = currentPos + new Vector3(Random.Range(-shakeStrength, shakeStrength), 0f, 0f);

            yield return new WaitForSeconds(shakeDuration);
        }

        transform.position = currentPos;
    }
}

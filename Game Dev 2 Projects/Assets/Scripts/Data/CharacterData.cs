using UnityEngine;

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
    [Header("Animation")]
    public Animator anim;
}

using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    public Vector3 pos1, pos2;
    private float lerpValue = 0f;
    public float speed = 1f;
    private bool toPos1 = false;

    void Update()
    {
        if (toPos1)
        {
            lerpValue += Time.deltaTime * speed;
            if(lerpValue >= 1f)
            {
                toPos1 = false;
            }
        }
        else
        {
            lerpValue -= Time.deltaTime * speed;
            if (lerpValue <= 0f)
            {
                toPos1 = true;
            }
        }

        transform.position = Vector3.Lerp(pos1, pos2, lerpValue);
    }
}

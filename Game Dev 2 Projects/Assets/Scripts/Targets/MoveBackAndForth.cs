using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    private Vector3 startingPos;
    public Vector3 StartingPos { get { return startingPos; } set { startingPos = StartingPos; } }

    public float distance = 10f;
    public Vector3 axis = Vector3.zero;

    private float timer = 0f;
    public float timeToMove = 5f;

    private bool forth = true;

    private void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        Vector3 targetPos = startingPos;
        if (forth)
        {
            targetPos += Vector3.Lerp(Vector3.zero, axis * distance, timer / timeToMove);
        }
        else
        {
            targetPos += Vector3.Lerp(axis * distance, Vector3.zero, timer / timeToMove);
        }

        transform.position = targetPos;

        if(timer > timeToMove)
        {
            timer = 0f;
            forth = !forth;
        }
    }
}

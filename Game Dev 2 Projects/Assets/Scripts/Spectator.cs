using UnityEngine;

public class Spectator : MonoBehaviour
{
    private Crowd crowd;

    private float angle;
    private float startingYPos, yOffset;
    private float randomSpeed;

    private bool redTeam;

    private void Start()
    {
        crowd = FindAnyObjectByType<Crowd>();

        startingYPos = transform.position.y;
        randomSpeed = Random.Range(crowd.defaultSpeed - crowd.randomnessFactor, crowd.defaultSpeed + crowd.randomnessFactor);

        ChooseRandomTeam();
    }

    private void FixedUpdate()
    {
        float currentSpeed = crowd.defaultSpeed;
        if(redTeam && crowd.coinRed) { currentSpeed = crowd.cheeringSpeed; }
        else if(!redTeam && !crowd.coinRed) { currentSpeed = crowd.cheeringSpeed; }

        yOffset = startingYPos + crowd.maximumHeight;
        angle += currentSpeed * 0.1f * randomSpeed;

        Vector3 newPos = new Vector3(transform.position.x, yOffset + Mathf.Sin(angle) * crowd.maximumHeight, transform.position.z);
        transform.position = newPos;
    }

    private void ChooseRandomTeam()
    {
        redTeam = Random.Range(0, 2) == 0;

        Color randomColor = Color.white;
        if (redTeam)
        {
            randomColor = new Color(1f, Random.Range(0f, 0.3f), Random.Range(0f, 0.3f), 1f);
        }
        else
        {
            randomColor = new Color(0f, Random.Range(0.6f, 1f), Random.Range(0f, 0.5f), 1f);
        }
        GetComponent<MeshRenderer>().material.color = randomColor;
    }
}

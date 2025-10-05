using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public AnimationCurve birdsInScene;
    public GameObject birdPrefab;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (NeedsMoreBirds())
        {
            Instantiate(birdPrefab);
        }
    }

    private bool NeedsMoreBirds()
    {
        int birdsNeeded = (int) birdsInScene.Evaluate(timer);
        int birdsHave = GameObject.FindObjectsByType<Bird>(FindObjectsSortMode.None).Length;

        return birdsHave < birdsNeeded;
    }
}

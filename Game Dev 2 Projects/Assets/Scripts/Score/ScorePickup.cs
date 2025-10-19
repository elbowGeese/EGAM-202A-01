using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScorePickup : MonoBehaviour
{
    public int scoreAdd = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PacmanController>() != null)
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        // add to score
        FindFirstObjectByType<ScoreHandler>().AddToScore(scoreAdd, transform.position);

        // destroy this
        Destroy(gameObject);
    }
}

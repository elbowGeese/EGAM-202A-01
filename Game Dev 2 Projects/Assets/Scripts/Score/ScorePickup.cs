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
        FindFirstObjectByType<ScoreHandler>().AddToScore(scoreAdd);
        Destroy(gameObject);
    }
}

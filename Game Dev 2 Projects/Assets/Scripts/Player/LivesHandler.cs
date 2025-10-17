using UnityEngine;

public class LivesHandler : MonoBehaviour
{
    // VARIABLES
    public int lives = 3;

    // REFERENCES
    private PowerUpHandler powerUpHandler;

    private void Start()
    {
        powerUpHandler = FindFirstObjectByType<PowerUpHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GhostBehaviour>() != null)
        {
            GhostBehaviour ghostBehaviour = other.gameObject.GetComponent<GhostBehaviour>();
            GhostBehaviour.GhostState ghostState = ghostBehaviour.state;
            switch (ghostState)
            {
                case GhostBehaviour.GhostState.CHASE:
                    Damage();
                    break;
                case GhostBehaviour.GhostState.SCATTER:
                    // eat
                    ghostBehaviour.state = GhostBehaviour.GhostState.RETURN_HOME;
                    break;
                default:
                    Debug.Log("Can't interact with this.");
                    break;
            }
        }
    }

    public void Damage()
    {
        lives--;

        if(lives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // end game
        Debug.Log("YOU DIED!");
    }
}

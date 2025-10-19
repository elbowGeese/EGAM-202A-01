using UnityEngine;

public class LivesHandler : MonoBehaviour
{
    // VARIABLES
    public int lives = 3;

    // REFERENCES
    private PowerUpHandler powerUpHandler;
    private LivesDisplay livesDisplay;
    private ScoreHandler scoreHandler;

    private void Start()
    {
        powerUpHandler = FindFirstObjectByType<PowerUpHandler>();
        livesDisplay = FindFirstObjectByType<LivesDisplay>();
        scoreHandler = FindFirstObjectByType<ScoreHandler>();
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
                    scoreHandler.EatGhost(other.transform.position);
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
        livesDisplay.SetDisplay(lives);

        if(lives <= 0)
        {
            FindFirstObjectByType<GameManager>().EndGame();
        }
        else
        {
            FindFirstObjectByType<GameManager>().ResetGame();
        }
    }
}

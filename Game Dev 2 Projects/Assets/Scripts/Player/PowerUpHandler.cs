using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    private bool isPoweredUp = false;
    public bool IsPoweredUp { get { return isPoweredUp; } }

    public float poweredTime;
    private float timer;

    private GhostBehaviour[] ghosts;
    private ScoreHandler scoreHandler;

    private void Start()
    {
        ghosts = FindObjectsByType<GhostBehaviour>(FindObjectsSortMode.None);
        scoreHandler = FindFirstObjectByType<ScoreHandler>();
    }

    private void Update()
    {
        if (isPoweredUp)
        {
            timer += Time.deltaTime;

            if(timer > poweredTime)
            {
                PowerDown();
            }
        }
    }

    public void PowerUp()
    {
        isPoweredUp = true;
        timer = 0f;

        scoreHandler.ghostStreak = 0;

        foreach (GhostBehaviour ghost in ghosts)
        {
            ghost.state = GhostBehaviour.GhostState.SCATTER;
        }
    }

    private void PowerDown()
    {
        isPoweredUp = false;

        foreach (GhostBehaviour ghost in ghosts)
        {
            if(ghost.state == GhostBehaviour.GhostState.SCATTER)
            {
                ghost.state = GhostBehaviour.GhostState.CHASE;
            }
        }
    }
}

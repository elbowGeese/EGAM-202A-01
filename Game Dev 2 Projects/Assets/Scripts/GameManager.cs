using UnityEngine;

public class GameManager : MonoBehaviour
{
    // VARIABLES
    private bool isPlaying = false;
    public float startTime = 10f;
    public float resetTime = 5f;
    private float timeToPlay;
    private float timer = 0f;

    // REFERENCES
    private PacmanController pacman;
    private GhostBehaviour[] ghosts;

    public GameObject startText, gameOverText;

    void Start()
    {
        pacman = FindFirstObjectByType<PacmanController>();
        ghosts = FindObjectsByType<GhostBehaviour>(FindObjectsSortMode.None);

        gameOverText.SetActive(false);
        timeToPlay = startTime;

        ResetGame();
    }

    void Update()
    {
        if (!isPlaying)
        {
            timer += Time.unscaledDeltaTime;
            if (timer > timeToPlay)
            {
                PlayGame();
            }
        }
    }

    public void ResetGame()
    {
        isPlaying = false;
        Time.timeScale = 0f;
        timer = 0f;

        startText.SetActive(true);

        // reset pacman 
        pacman.ResetPacman();
        // reset ghosts
        foreach (GhostBehaviour ghost in ghosts)
        {
            ghost.ResetGhost();
        }
    }

    private void PlayGame()
    {
        isPlaying = true;
        Time.timeScale = 1f;

        startText.SetActive(false);
        pacman.ReleasePacman();
        foreach (GhostBehaviour ghost in ghosts)
        {
            ghost.ReleaseGhost();
        }

        timeToPlay = resetTime;
    }

    public void EndGame()
    {
        gameOverText.SetActive(true);
        Time.timeScale = 0f;

        // destroy pacman and ghosts
        Destroy(pacman.gameObject);
        foreach (GhostBehaviour ghost in ghosts)
        {
            Destroy(ghost.gameObject);
        }
    }
}

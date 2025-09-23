using UnityEngine;

public class PauseHandler : MonoBehaviour
{

    private void Start()
    {
        PlayGame();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
}

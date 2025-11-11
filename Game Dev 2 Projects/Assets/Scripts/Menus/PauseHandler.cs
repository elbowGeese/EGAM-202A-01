using UnityEngine;

public class PauseHandler : MonoBehaviour
{

    private void Start()
    {
        PlayGame();
    }

    public void PlayGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
}

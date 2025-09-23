using UnityEngine;
using UnityEngine.InputSystem;

public class PauseInput : MonoBehaviour
{
    InputAction pauseAction;

    public PauseHandler pauseHandler;
    public MenuToggle menuToggle;

    void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    void Update()
    {
        if (pauseAction != null)
        {
            if (pauseAction.WasPressedThisFrame())
            {
                if (Time.timeScale > 0f) 
                {
                    PauseGame();
                }
                else 
                {
                    PlayGame();
                }
            }
        }
    }

    public void PlayGame()
    {
        menuToggle.ToggleMenu(1);
        pauseHandler.PlayGame();
    }

    public void PauseGame()
    {
        menuToggle.ToggleMenu(0);
        pauseHandler.PauseGame();
    }
}

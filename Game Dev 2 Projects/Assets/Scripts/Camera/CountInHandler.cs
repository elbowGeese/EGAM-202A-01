using UnityEngine;

public class CountInHandler : MonoBehaviour
{
    public CameraHandler camHandler;

    public BubbleGumGun bubbleGumGun;
    public TankController tankController;
    public Timer timer;

    void Start()
    {
        camHandler.SetState(CameraHandler.CameraState.OVERVIEW);

        bubbleGumGun.isPaused = true;
        tankController.isPaused = true;
        timer.isPaused = true;
    }

    public void StartGame()
    {
        camHandler.SetState(CameraHandler.CameraState.MAIN);

        bubbleGumGun.isPaused = false;
        tankController.isPaused = false;
        timer.isPaused = false;
    }
}

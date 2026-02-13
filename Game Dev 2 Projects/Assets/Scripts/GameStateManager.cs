using TMPro;
using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    public enum GameState { Beginning, Playing, Ended }
    public GameState State { get; private set; }

    CoinBehaviour coin;
    PlayerMovement plyMove;
    CameraManager cameraManager;
    MainTimer mainTimer;

    public TMP_Text countdownText;
    public EndScreenBehaviour endScreenBehaviour;
    public CameraGeometryFade cameraGeometryFade;

    public AudioSource countdownSFX, goSFX;

    void Start()
    {
        coin = FindAnyObjectByType<CoinBehaviour>();
        plyMove = FindAnyObjectByType<PlayerMovement>();
        cameraManager = FindAnyObjectByType<CameraManager>();
        mainTimer = FindAnyObjectByType<MainTimer>();

        SetState(State);
    }

    public void SetState(GameState state)
    {
        State = state;

        switch (State)
        {
            case GameState.Beginning:
                StartCoroutine(SetBeginningState());
                break;
            case GameState.Playing:
                SetPlayingState();
                break;
            case GameState.Ended:
                StartCoroutine(SetEndedState());
                break;
            default:
                Debug.Log("Unknown Game State set.");
                break;
        }
    }

    IEnumerator SetBeginningState()
    {
        PauseMainMovers(true);
        // show countdown
        cameraManager.SetPriorityCamera(CameraManager.CameraType.PlyCam);
        countdownSFX.Play();
        countdownText.text = "3";

        yield return new WaitForSeconds(1f);

        cameraManager.SetPriorityCamera(CameraManager.CameraType.CoinCam);
        countdownSFX.Play();
        countdownText.text = "2";

        yield return new WaitForSeconds(1f);

        cameraManager.SetPriorityCamera(CameraManager.CameraType.MainCam);
        countdownSFX.Play();
        countdownText.text = "1";

        yield return new WaitForSeconds(1f);

        countdownText.text = "";
        goSFX.Play();
        SetState(GameState.Playing);
    }

    private void SetPlayingState()
    {
        PauseMainMovers(false);
        cameraManager.SetPriorityCamera(CameraManager.CameraType.MainCam);
    }

    IEnumerator SetEndedState()
    {
        PauseMainMovers(true);

        yield return new WaitForSeconds(1f);

        // show ending ui
        cameraManager.SetPriorityCamera(CameraManager.CameraType.EndCoinCam);
        cameraGeometryFade.FadeBlockingGeometry();
        yield return endScreenBehaviour.ShowEndScreen(coin.IsCoinFacingUp());
    }

    private void PauseMainMovers(bool pause)
    {
        // pause coin
        coin.PauseCoin(pause);
        // pause ply movement
        plyMove.isPaused = pause;
        // pause timer
        mainTimer.isPaused = pause;
    }
}

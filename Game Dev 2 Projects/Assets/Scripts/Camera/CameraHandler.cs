using UnityEngine;
using Unity.Cinemachine;

public class CameraHandler : MonoBehaviour
{
    public CinemachineCamera overviewCam, mainCam;

    public enum CameraState { OVERVIEW, MAIN };
    public CameraState state;

    public void SetState(CameraState newState)
    {
        if (state == newState) return;

        switch (newState)
        {
            case CameraState.OVERVIEW:
                overviewCam.Prioritize(); 
                break;
            case CameraState.MAIN:
                mainCam.Prioritize(); 
                break;
            default:
                Debug.Log("Unknown camera state, please add camera and state to enum."); 
                break;
        }

        state = newState;
    }
}

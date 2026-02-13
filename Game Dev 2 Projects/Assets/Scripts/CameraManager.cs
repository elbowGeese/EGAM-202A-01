using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera[] cameras;
    private int cameraIndex = 0;
    public enum CameraType { CoinCam, PlyCam, MainCam, EndCoinCam }

    public void SetPriorityCamera(CameraType camType)
    {
        cameraIndex = (int)camType;
        cameras[cameraIndex].Prioritize();
    }

    public void SetNextCameraAsPriority()
    {
        cameraIndex = (cameraIndex + 1) % cameras.Length;
        cameras[cameraIndex].Prioritize();
    }
}

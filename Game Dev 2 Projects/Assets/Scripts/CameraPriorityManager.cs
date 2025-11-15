using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class CameraPriorityManager : MonoBehaviour
{
    public List<CinemachineCamera> cameras;
    private int cameraIndex = 0;

    void Start()
    {
        SetPriority(0);
    }

    public void SetPriority(int index)
    {
        cameraIndex = index;
        for (int i = 0; i < cameras.Count; i++) 
        { 
            bool isMatch = i == cameraIndex;
            if (isMatch) { cameras[i].Priority = 100; }
            else {  cameras[i].Priority = 0; }
        }
    }
}

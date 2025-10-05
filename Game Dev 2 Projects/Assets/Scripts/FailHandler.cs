using System.Collections;
using UnityEngine;

public class FailHandler : MonoBehaviour
{
    public Animator hitBirdDisplayAnim;
    public float closingTime = 2f;

    public CameraHandler cameraHandler;

    public void EnterFailState()
    {
        StartCoroutine(FailState());
    }

    IEnumerator FailState()
    {
        hitBirdDisplayAnim.SetTrigger("timesUp");
        cameraHandler.SetState(CameraHandler.CameraState.OVERVIEW);

        yield return new WaitForSeconds(closingTime);

        GetComponent<SceneChanges>().LoadToSceneByIndex(3);
    }
}

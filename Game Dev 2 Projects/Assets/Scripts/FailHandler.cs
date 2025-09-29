using System.Collections;
using UnityEngine;

public class FailHandler : MonoBehaviour
{
    public Animator hitBirdDisplayAnim;
    public float closingTime = 2f;

    public void EnterFailState()
    {
        StartCoroutine(FailState());
    }

    IEnumerator FailState()
    {
        hitBirdDisplayAnim.SetTrigger("timesUp");

        yield return new WaitForSeconds(closingTime);

        GetComponent<SceneChanges>().LoadToSceneByIndex(3);
    }
}

using System.Collections;
using UnityEngine;

public class FailHandler : MonoBehaviour
{   
    public Animator hitBirdDisplayAnim;
    public float stunTime = 1f;

    public TankController tankController;
    public BubbleGumGun bubbleGumGun;

    public void EnterFailState()
    {
        StartCoroutine(FailState());
    }

    // stuns player for a second
    IEnumerator FailState()
    {
        // set player controls to paused
        tankController.SetStun(true);
        bubbleGumGun.isPaused = true;

        // wait
        yield return new WaitForSeconds(stunTime);

        // unlock player controls
        tankController.SetStun(false);
        bubbleGumGun.isPaused = false;
    }
}

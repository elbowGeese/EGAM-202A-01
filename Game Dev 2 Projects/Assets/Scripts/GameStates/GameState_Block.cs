using UnityEngine;
using System;
using System.Collections;
using Unity.Cinemachine;

public class GameState_Block : GameState
{
    // game state data
    public event Action OnStateOver;
    public string buttonMessage { get; set; }
    public Camera Camera { get; set; }
    private CinemachineImpulseSource impulseSource;
    private CameraPriorityManager camPriorityManager;
    public CharacterData mario { get; set; }
    public CharacterData goomba { get; set; }

    bool blockWindowOpen = false;
    bool successfulBlock = false;
    bool failedBlock = false;

    public void BeginState()
    {
        Debug.Log("Began Blocking State!");

        if (impulseSource == null)
        {
            impulseSource = Camera.gameObject.GetComponent<CinemachineImpulseSource>();
        }

        if(camPriorityManager == null)
        {
            camPriorityManager = Camera.gameObject.GetComponent<CameraPriorityManager>();
        }
    }

    public IEnumerator StateRoutine()
    {
        yield return new WaitForSeconds(goomba.jumpDelay + 0.5f);

        // approach
        camPriorityManager.SetPriority(1);
        goomba.anim.SetBool("walking", true);
        float approachProgress = 0;
        float totalDist = Math.Abs(Vector3.Distance(goomba.startPos, mario.startPos));
        float targetDist = totalDist - (goomba.width / 2) - (mario.width / 2);
        float targetProgress = targetDist / totalDist;

        while (approachProgress < targetProgress)
        {
            approachProgress = Mathf.Clamp01(approachProgress + (Time.deltaTime * goomba.moveSpeed));
            goomba.transform.position = Vector3.Lerp(goomba.startPos, mario.startPos, approachProgress);
            yield return null;
        }
        goomba.anim.SetBool("walking", false);

        yield return new WaitForSeconds(goomba.jumpDelay);

        // jump up
        goomba.anim.SetBool("attacking", true);
        float jumpProgress = 0f;
        float goombaStartY = goomba.transform.position.y;
        float remainingProgress = 1f - approachProgress;
        while (jumpProgress < 1f)
        {
            // calc y
            jumpProgress = Mathf.Clamp01(jumpProgress + (Time.deltaTime * goomba.jumpSpeed.Evaluate(jumpProgress)));
            float targetY = Mathf.Lerp(goombaStartY, goomba.maxJumpHeight, jumpProgress);

            // calc pos
            Vector3 targetPos = Vector3.Lerp(goomba.startPos, mario.startPos, Mathf.Clamp01(approachProgress + (remainingProgress * jumpProgress)));
            targetPos.y = targetY;
            goomba.transform.position = targetPos;

            yield return null;
        }
        Vector3 targetJumpPos = mario.startPos;
        targetJumpPos.y = goomba.maxJumpHeight;
        goomba.transform.position = targetJumpPos;

        // block
        yield return BlockRoutine();

        // return to start
        float fallProgress = 0f;
        while (fallProgress < 1f)
        {
            // calc y
            fallProgress = Mathf.Clamp01(fallProgress + (Time.deltaTime * goomba.fallSpeed.Evaluate(fallProgress)));
            float targetY = Mathf.Lerp(goomba.maxJumpHeight, goombaStartY, fallProgress);

            // calc pos
            Vector3 targetPos = Vector3.Lerp(goomba.startPos, mario.startPos, Mathf.Clamp01(1f - (remainingProgress * fallProgress)));
            targetPos.y = targetY;
            goomba.transform.position = targetPos;

            yield return null;
        }

        yield return new WaitForSeconds(goomba.jumpDelay);

        camPriorityManager.SetPriority(0);
        goomba.anim.SetBool("walking", true);
        while (approachProgress > 0f)
        {
            approachProgress = Mathf.Clamp01(approachProgress - (Time.deltaTime * mario.moveSpeed));
            goomba.transform.position = Vector3.Lerp(goomba.startPos, mario.startPos, approachProgress);
            yield return null;
        }
        goomba.transform.position = goomba.startPos;

        mario.anim.SetBool("squashed", false);
        mario.anim.SetBool("blocked", false);
        goomba.anim.SetBool("walking", false);

        OnStateOver?.Invoke();
    }

    IEnumerator BlockRoutine()
    {
        successfulBlock = false;
        failedBlock = false;
        bool windowOpened = false;

        // fall
        float fallProgress = 0f;
        while (fallProgress < 1f)
        {
            fallProgress = Mathf.Clamp01(fallProgress + (Time.deltaTime * goomba.fallSpeed.Evaluate(fallProgress)));
            float targetY = Mathf.Lerp(goomba.maxJumpHeight, mario.height, fallProgress);
            Vector3 fallTargetPos = mario.startPos;
            fallTargetPos.y = targetY;
            goomba.transform.position = fallTargetPos;

            if (fallProgress > 1f - goomba.attackWindowPercent && !windowOpened)
            {
                blockWindowOpen = true;
                windowOpened = true;
            }

            yield return null;
        }
        Vector3 finalFallTargetPos = mario.startPos;
        finalFallTargetPos.y = mario.height;
        goomba.transform.position = finalFallTargetPos;

        goomba.anim.SetBool("attacking", false);

        // jump back up
        float bounceProgress = 0f;
        bool showedFeedback = false;
        while (bounceProgress < 1f)
        {
            bounceProgress = Mathf.Clamp01(bounceProgress + (Time.deltaTime * goomba.jumpSpeed.Evaluate(bounceProgress)));
            float targetY = Mathf.Lerp(mario.height, goomba.maxJumpHeight, bounceProgress);
            Vector3 bounceTargetPos = mario.startPos;
            bounceTargetPos.y = targetY;
            goomba.transform.position = bounceTargetPos;

            if(bounceProgress >= 0.2f && !showedFeedback)
            {
                // success feedback
                blockWindowOpen = false;
                if (!successfulBlock || failedBlock) { FailFeedback(); }
                else { SuccessFeedback(); }

                showedFeedback = true;
            }

            yield return null;
        }
        Vector3 finalBounceTargetPos = mario.startPos;
        finalBounceTargetPos.y = goomba.maxJumpHeight;
        goomba.transform.position = finalBounceTargetPos;
    }

    public Vector3 GetFeedbackPosition()
    {
        Vector3 feedbackPos = mario.startPos + -Camera.transform.forward;
        feedbackPos.y = mario.height;

        return feedbackPos;
    }

    // player successfully blocks
    public void SuccessFeedback()
    {
        Vector3 feedbackPos = GetFeedbackPosition();

        // small impact particle
        GameObject particle = GameObject.Instantiate(goomba.successAtkParticle);
        particle.transform.position = feedbackPos;

        // damage popup
        GameObject dmgPopup = GameObject.Instantiate(goomba.dmgPopup);
        dmgPopup.transform.position = feedbackPos + (-Camera.transform.forward * 1.1f);
        dmgPopup.GetComponent<DMGPopup>().SetDMGText("1");

        // block text
        mario.anim.SetBool("blocked", true);

        // screen shake
        impulseSource.GenerateImpulse();

        // shake mario
        mario.Shake(0.2f);
    }

    // player fails to block
    public void FailFeedback()
    {
        Vector3 feedbackPos = GetFeedbackPosition();

        // larger impact particle
        GameObject particle = GameObject.Instantiate(goomba.failAtkParticle);
        particle.transform.position = feedbackPos;

        // damage popup
        GameObject dmgPopup = GameObject.Instantiate(goomba.dmgPopup);
        dmgPopup.transform.position = feedbackPos + (-Camera.transform.forward * 1.1f);
        dmgPopup.GetComponent<DMGPopup>().SetDMGText("2");

        // screen shake
        impulseSource.GenerateImpulse();

        // squash mario
        mario.anim.SetBool("squashed", true);
    }

    public void StateButton()
    {
        if (blockWindowOpen)
        {
            Success();
        }
        else
        {
            Fail();
        }
    }

    private void Success()
    {
        blockWindowOpen = false;
        successfulBlock = true;
    }

    private void Fail()
    {
        failedBlock = true;
    }

    public void EndState()
    {
        Debug.Log("Ended Blocking State!");
    }
}

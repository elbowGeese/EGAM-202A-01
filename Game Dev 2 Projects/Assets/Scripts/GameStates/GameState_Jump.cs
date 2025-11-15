using UnityEngine;
using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.Cinemachine;

public class GameState_Jump : GameState
{
    // game state data
    public event Action OnStateOver;
    public string buttonMessage { get; set; }
    public Camera Camera { get; set; }
    private CinemachineImpulseSource impulseSource;
    public CharacterData mario { get; set; }
    public CharacterData goomba { get; set; }

    private bool attackWindowOpen = false;
    private bool successfulAttack = false;
    private bool failedAttack = false;

    public void BeginState()
    {
        Debug.Log("Began Jumping State!");

        if(impulseSource == null)
        {
            impulseSource = Camera.gameObject.GetComponent<CinemachineImpulseSource>();
        }
    }

    public IEnumerator StateRoutine()
    {
        yield return new WaitForSeconds(mario.jumpDelay);

        // approach
        mario.anim.SetBool("walking", true);
        float approachProgress = 0;
        float totalDist = Math.Abs(Vector3.Distance(mario.startPos, goomba.startPos));
        float targetDist = totalDist - (mario.width / 2) - (goomba.width / 2);
        float targetProgress = targetDist / totalDist;

        while (approachProgress < targetProgress)
        {
            approachProgress = Mathf.Clamp01(approachProgress + (Time.deltaTime * mario.moveSpeed));
            mario.transform.position = Vector3.Lerp(mario.startPos, goomba.startPos, approachProgress);
            yield return null;
        }
        mario.anim.SetBool("walking", false);

        yield return new WaitForSeconds(mario.jumpDelay);

        // jump up
        mario.anim.SetInteger("jumps", 1);
        float jumpProgress = 0f;
        float marioStartY = mario.transform.position.y;
        float remainingProgress = 1f - approachProgress;
        while (jumpProgress < 1f)
        {
            // calc y
            jumpProgress = Mathf.Clamp01(jumpProgress + (Time.deltaTime * mario.jumpSpeed.Evaluate(jumpProgress)));
            float targetY = Mathf.Lerp(marioStartY, mario.maxJumpHeight, jumpProgress);
       
            // calc pos
            Vector3 targetPos = Vector3.Lerp(mario.startPos, goomba.startPos, Mathf.Clamp01(approachProgress + (remainingProgress * jumpProgress)));
            targetPos.y = targetY;
            mario.transform.position = targetPos;

            yield return null;
        }
        Vector3 targetJumpPos = goomba.startPos;
        targetJumpPos.y = mario.maxJumpHeight;
        mario.transform.position = targetJumpPos;

        // yield return new WaitForSeconds(mario.jumpDelay);

        // attacks
        int attackStreak = 0; 
        while(attackStreak < 5)
        {
            yield return AttackRoutine(attackStreak);

            if (!successfulAttack || failedAttack)
            {
                attackStreak = 5;
                break;
            }

            attackStreak++;
        }

        // return to start
        float fallProgress = 0f;
        while (fallProgress < 1f)
        {
            // calc y
            fallProgress = Mathf.Clamp01(fallProgress + (Time.deltaTime * mario.fallSpeed.Evaluate(fallProgress)));
            float targetY = Mathf.Lerp(mario.maxJumpHeight, marioStartY, fallProgress);

            // calc pos
            Vector3 targetPos = Vector3.Lerp(mario.startPos, goomba.startPos, Mathf.Clamp01(1f - (remainingProgress * fallProgress)));
            targetPos.y = targetY;
            mario.transform.position = targetPos;

            yield return null;
        }
        mario.anim.SetInteger("jumps", 0);

        yield return new WaitForSeconds(mario.jumpDelay);

        mario.anim.SetBool("walking", true);
        while (approachProgress > 0f)
        {
            approachProgress = Mathf.Clamp01(approachProgress - (Time.deltaTime * mario.moveSpeed));
            mario.transform.position = Vector3.Lerp(mario.startPos, goomba.startPos, approachProgress);
            yield return null;
        }
        mario.transform.position = mario.startPos;

        mario.anim.SetBool("walking", false);
        goomba.anim.SetBool("isSquashed", false);

        OnStateOver?.Invoke();
    }

    IEnumerator AttackRoutine(int attackStreak)
    {
        successfulAttack = false;
        failedAttack = false;
        bool windowOpened = false;

        // fall
        float fallProgress = 0f;
        while (fallProgress < 1f)
        {
            fallProgress = Mathf.Clamp01(fallProgress + (Time.deltaTime * mario.fallSpeed.Evaluate(fallProgress)));
            float targetY = Mathf.Lerp(mario.maxJumpHeight, goomba.height, fallProgress);
            Vector3 fallTargetPos = goomba.startPos;
            fallTargetPos.y = targetY;
            mario.transform.position = fallTargetPos;

            if (fallProgress > 1f - mario.attackWindowPercent && !windowOpened)
            {
                attackWindowOpen = true;
                windowOpened = true;
            }

            yield return null;
        }
        Vector3 finalFallTargetPos = goomba.startPos;
        finalFallTargetPos.y = goomba.height;
        mario.transform.position = finalFallTargetPos;

        // jump back up
        float bounceProgress = 0f;
        bool shownFeedback = false;
        while (bounceProgress < 1f)
        {
            bounceProgress = Mathf.Clamp01(bounceProgress + (Time.deltaTime * mario.jumpSpeed.Evaluate(bounceProgress)));
            float targetY = Mathf.Lerp(goomba.height, mario.maxJumpHeight, bounceProgress);
            Vector3 bounceTargetPos = goomba.startPos;
            bounceTargetPos.y = targetY;
            mario.transform.position = bounceTargetPos;

            if(bounceProgress >= 0.2f && !shownFeedback)
            {
                attackWindowOpen = false;
                // success feedback
                if (!successfulAttack || failedAttack) { FailFeedback(); }
                else { SuccessFeedback(attackStreak); }

                shownFeedback = true;
            }

            yield return null;
        }
        Vector3 finalBounceTargetPos = goomba.startPos;
        finalBounceTargetPos.y = mario.maxJumpHeight;
        mario.transform.position = finalBounceTargetPos;
    }

    public Vector3 GetFeedbackPosition()
    {
        Vector3 feedbackPos = goomba.startPos + -Camera.transform.forward;
        feedbackPos.y = goomba.height;

        return feedbackPos;
    }

    public void SuccessFeedback(int attackStreak)
    {
        Vector3 feedbackPos = GetFeedbackPosition();

        // impact particle
        GameObject particle = GameObject.Instantiate(mario.successAtkParticle);
        particle.transform.position = feedbackPos;

        // dmg popup
        GameObject dmgPopup = GameObject.Instantiate(mario.dmgPopup);
        dmgPopup.transform.position = feedbackPos + (-Camera.transform.forward * 1.1f);
        if(attackStreak == 0) { dmgPopup.GetComponent<DMGPopup>().SetDMGText("5"); }
        else { dmgPopup.GetComponent<DMGPopup>().SetDMGText("1"); }

        // combo name
        ComboPopup combo = GameObject.FindAnyObjectByType<ComboPopup>();
        if (combo != null)
        {
            if(attackStreak == 3) { combo.ShowCombo("EXCELLENT", Color.orange); }
            else if(attackStreak < 3) { combo.ShowCombo("NICE", Color.green); }
        }

        // camera shake
        impulseSource.GenerateImpulse();

        // goomba squash
        goomba.anim.SetBool("isSquashed", true);

        // mario animation
        mario.anim.SetInteger("jumps", attackStreak + 2);
    }

    public void FailFeedback()
    {
        Vector3 feedbackPos = GetFeedbackPosition();

        // impact particle
        GameObject particle = GameObject.Instantiate(mario.failAtkParticle);
        particle.transform.position = feedbackPos;

        // damage popup
        GameObject dmgPopup = GameObject.Instantiate(mario.dmgPopup);
        dmgPopup.transform.position = feedbackPos + (-Camera.transform.forward * 1.1f);
        dmgPopup.GetComponent<DMGPopup>().SetDMGText("1");

        // combo same as previous
        // the combo popup stays for 2 jumps by default so im not gonna call anything

        // lesser screen shake
        impulseSource.GenerateImpulse(new Vector3(0.1f, -0.1f, 0f));
    }

    public void StateButton()
    {
        if (attackWindowOpen)
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
        attackWindowOpen = false;
        successfulAttack = true;
    }

    private void Fail()
    {
        failedAttack = true;
    }

    public void EndState()
    {
        Debug.Log("Ended Jumping State!");
    }
}

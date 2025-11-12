using UnityEngine;
using System;
using System.Collections;
using JetBrains.Annotations;

public class GameState_Jump : GameState
{
    // game state data
    public event Action OnStateOver;
    public string buttonMessage { get; set; }
    public CharacterData mario { get; set; }
    public CharacterData goomba { get; set; }

    private bool attackWindowOpen = false;
    private bool successfulAttack = false;

    public void BeginState()
    {
        Debug.Log("Began Jumping State!");
    }

    public void UpdateState()
    {
        
    }

    public IEnumerator StateRoutine()
    {
        yield return new WaitForSeconds(mario.jumpDelay);

        // approach
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

        yield return new WaitForSeconds(mario.jumpDelay);

        // jump up
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
            yield return AttackRoutine();

            if (!successfulAttack)
            {
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

        yield return new WaitForSeconds(mario.jumpDelay);

        while (approachProgress > 0f)
        {
            approachProgress = Mathf.Clamp01(approachProgress - (Time.deltaTime * mario.moveSpeed));
            mario.transform.position = Vector3.Lerp(mario.startPos, goomba.startPos, approachProgress);
            yield return null;
        }
        mario.transform.position = mario.startPos;

        OnStateOver?.Invoke();
    }

    IEnumerator AttackRoutine()
    {
        successfulAttack = false;
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

        // success feedback

        // jump back up
        float bounceProgress = 0f;
        while (bounceProgress < 1f)
        {
            bounceProgress = Mathf.Clamp01(bounceProgress + (Time.deltaTime * mario.jumpSpeed.Evaluate(bounceProgress)));
            float targetY = Mathf.Lerp(goomba.height, mario.maxJumpHeight, bounceProgress);
            Vector3 bounceTargetPos = goomba.startPos;
            bounceTargetPos.y = targetY;
            mario.transform.position = bounceTargetPos;

            yield return null;
        }
        Vector3 finalBounceTargetPos = goomba.startPos;
        finalBounceTargetPos.y = mario.maxJumpHeight;
        mario.transform.position = finalBounceTargetPos;
    }

    public void StateButton()
    {
        if (attackWindowOpen)
        {
            Success();
        }
    }

    private void Success()
    {
        attackWindowOpen = false;
        successfulAttack = true;
    }

    private void Fail()
    {
        // OnStateOver?.Invoke();
    }

    public void EndState()
    {
        Debug.Log("Ended Jumping State!");
    }
}

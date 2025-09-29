using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinalScoreDisplyHandler : MonoBehaviour
{
    public GameObject highScoreDisplay, submitScoreDisplay, buttonsDisplay;
    public TMP_Text finalScoreText, highScoreText;
    public float waitTime = 2f;

    void Start()
    {
        highScoreDisplay.SetActive(false);
        submitScoreDisplay.SetActive(false);
        buttonsDisplay.SetActive(false);

        StartCoroutine(WalkThroughFinalDisplay());
    }

    IEnumerator WalkThroughFinalDisplay()
    {
        // display final score
        float displayScore = 0f;
        float finalScoreTimer = 0f;
        while(displayScore != ScoreData.previousScore)
        {
            finalScoreTimer += Time.deltaTime;
            displayScore = Mathf.Lerp(0f, ScoreData.previousScore, finalScoreTimer / waitTime);
            finalScoreText.text = displayScore.ToString("000000000");

            if (Mouse.current.leftButton.wasPressedThisFrame) { displayScore = ScoreData.previousScore; }

            yield return null;
        }

        finalScoreText.text = ScoreData.previousScore.ToString("000000000");

        // display high score
        highScoreDisplay.SetActive(true);
        highScoreText.text = ScoreData.personalBestScore.ToString("000000000");
        float highScoreTimer = 0f;
        while (highScoreTimer < (waitTime / 2))
        {
            highScoreTimer += Time.deltaTime;

            if (Mouse.current.leftButton.wasPressedThisFrame) { highScoreTimer = (waitTime / 2); }

            yield return null;
        }

        // display submit score
        submitScoreDisplay.SetActive(true);
        float submitScoreTimer = 0f;
        while (submitScoreTimer < (waitTime / 2))
        {
            submitScoreTimer += Time.deltaTime;

            if (Mouse.current.leftButton.wasPressedThisFrame) { submitScoreTimer = (waitTime / 2); }

            yield return null;
        }

        // display buttons
        buttonsDisplay.SetActive(true);
    }
}

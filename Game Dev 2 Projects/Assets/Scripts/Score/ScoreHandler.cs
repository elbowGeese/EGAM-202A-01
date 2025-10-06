using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText;

    private int streak = 0;
    public TMP_Text addText, multiText;
    public GameObject multiplierUI;
    private Animator addAnim, multiAnim;

    void Start()
    {
        scoreText.text = score.ToString("000000000");

        multiAnim = multiplierUI.GetComponent<Animator>();
        multiplierUI.SetActive(false);
        addAnim = addText.GetComponent<Animator>();
    }

    public void AddToScore(int amount)
    {
        int multiplier = 1;
        if(streak >= 1) { multiplier = streak; }
        addText.text = "+" + amount;
        addAnim.SetTrigger("show");
        
        score += (amount * multiplier);
        scoreText.text = score.ToString("000000000");

        AddToStreak();
    }

    private void AddToStreak()
    {
        streak++;

        if (!multiplierUI.activeSelf)
        {
            multiplierUI.SetActive(true);
        }

        multiText.text = "x" + streak;
        multiAnim.SetTrigger("boom");
    }

    public void BreakStreak()
    {
        streak = 0;
        if (multiplierUI.activeSelf)
        {
            multiplierUI.SetActive(false);
        }
    }

    public void SubmitScoreToData()
    {
        ScoreData.previousScore = score;

        if (ScoreData.personalBestScore != -1)
        {
            if(ScoreData.personalBestScore < score) 
            { 
                ScoreData.personalBestScore = score;
            }
        }
        else
        {
            ScoreData.personalBestScore = score;
        }
    }
}

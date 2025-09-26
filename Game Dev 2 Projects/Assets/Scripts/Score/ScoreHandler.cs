using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText;

    void Start()
    {
        scoreText.text = score.ToString("000000000");
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString("000000000");
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

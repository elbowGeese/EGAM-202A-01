using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private int score;
    public int Score { get { return score; } }

    public TMP_Text scoreDisplay;

    private void Start()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        score = 0;
        scoreDisplay.text = score.ToString();
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreDisplay.text = score.ToString();
    }

    public void SaveScore()
    {
        int currentHighscore = PlayerPrefs.GetInt("Highscore");

        if (score > currentHighscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}

using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private int score;
    public int Score { get { return score; } }

    public TMP_Text scoreDisplay;
    public int ghostStreak = 0;

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

    public void EatGhost()
    {
        ghostStreak++;

        switch (ghostStreak)
        {
            case 1:
                AddToScore(200);
                break;
            case 2:
                AddToScore(400);
                break;
            case 3:
                AddToScore(800);
                break;
            case 4:
                AddToScore(1600);
                break;
            default:
                Debug.Log("Unknown streak given.");
                break;
        }
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

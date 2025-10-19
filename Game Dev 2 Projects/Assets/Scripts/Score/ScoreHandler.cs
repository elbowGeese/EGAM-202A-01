using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private int score;
    public int Score { get { return score; } }

    public TMP_Text scoreDisplay;
    public int ghostStreak = 0;

    public GameObject scorePopupPrefab;

    private void Start()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        score = 0;
        scoreDisplay.text = score.ToString();
    }

    public void AddToScore(int amount, Vector3 position)
    {
        if(amount > 100)
        {
            // spawn popup
            GameObject popup = Instantiate(scorePopupPrefab);
            popup.transform.position = position;
            popup.GetComponent<ScorePopup>().scoreText.text = amount.ToString();
        }

        // add to score
        score += amount;
        scoreDisplay.text = score.ToString();
    }

    public void EatGhost(Vector3 position)
    {
        ghostStreak++;

        switch (ghostStreak)
        {
            case 1:
                AddToScore(200, position);
                break;
            case 2:
                AddToScore(400, position);
                break;
            case 3:
                AddToScore(800, position);
                break;
            case 4:
                AddToScore(1600, position);
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

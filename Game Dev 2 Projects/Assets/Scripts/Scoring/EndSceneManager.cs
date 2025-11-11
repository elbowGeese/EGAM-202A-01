using TMPro;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    public GameObject winDisplay, loseDisplay;
    public TMP_Text finalScore;

    void Start()
    {
        finalScore.text = "Final Score: " + ScoreManager.Score.ToString();

        if (ScoreManager.Won)
        {
            winDisplay.SetActive(true);
            loseDisplay.SetActive(false);
        }
        else
        {
            winDisplay.SetActive(false);
            loseDisplay.SetActive(true);
        }
    }
}

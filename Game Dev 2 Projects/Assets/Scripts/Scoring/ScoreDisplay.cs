using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text textDisplay;

    void Start()
    {
        textDisplay = GetComponent<TMP_Text>();
        ScoreManager.ResetScore();
    }

    void Update()
    {
        textDisplay.text = "Score: " + ScoreManager.Score.ToString();
    }
}

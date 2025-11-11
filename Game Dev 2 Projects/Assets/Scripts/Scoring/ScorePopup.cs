using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    public TMP_Text scoreDisplay, rankDisplay;
    
    public void SetText(string scoreText, string rank, Color rankColor)
    {
        scoreDisplay.text = scoreText;
        rankDisplay.text = rank;
        rankDisplay.color = rankColor;
    }

    public void DestroyFromAnim()
    {
        Destroy(gameObject);
    }
}

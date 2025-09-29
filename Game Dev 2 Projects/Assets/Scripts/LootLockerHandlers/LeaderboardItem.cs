using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class LeaderboardItem : MonoBehaviour
{
    public TMP_Text playerText, scoreText;

    public void SetItemData(string playerName, string playerScore)
    {
        playerText.text = playerName;
        scoreText.text = playerScore;
    }
}

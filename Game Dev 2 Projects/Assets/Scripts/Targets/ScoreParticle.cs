using UnityEngine;
using TMPro;

public class ScoreParticle : MonoBehaviour
{
    public TMP_Text scoreText;

    public void SetScoreText(int score)
    {
        scoreText.text = "+" + score.ToString();
    }

    public void DestroyFromAnim()
    {
        Destroy(gameObject);
    }
}

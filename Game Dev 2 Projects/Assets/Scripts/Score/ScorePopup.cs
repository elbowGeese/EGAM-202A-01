using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    public TMP_Text scoreText;
    public float timeToDestroy = 2f;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}

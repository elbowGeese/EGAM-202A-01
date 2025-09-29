using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSubmit : MonoBehaviour
{
    public TMP_InputField inputField;
    public Leaderboard leaderboard;

    public GameObject submitButton;
    public Image statusDisplay;
    public Sprite doneSprite;

    public void SubmitInput()
    {
        StartCoroutine(SubmitInputRoutine(inputField.text));
    }

    IEnumerator SubmitInputRoutine(string playerName)
    {
        submitButton.SetActive(false);
        inputField.interactable = false;
        yield return leaderboard.SubmitScoreRoutine(playerName, ScoreData.previousScore);

        statusDisplay.sprite = doneSprite;
    }
}

using UnityEngine;
using TMPro;

public class NameDisplay : MonoBehaviour
{
    public TMP_Text textDisplay;
    public GameObject display;

    public void SetText(string message)
    {
        textDisplay.text = message;
    }

    public void ShowDisplay()
    {
        display.SetActive(true);
    }

    public void HideDisplay()
    {
        display.SetActive(false);
    }
}

using TMPro;
using UnityEngine;

public class MainTimer : MonoBehaviour
{
    public bool isPaused = true;
    public float timePassed { get; private set; }
    private TMP_Text timerLabel;

    void Start()
    {
        timePassed = 0f;
        timerLabel = GetComponent<TMP_Text>();

        UpdateTimerLabel();
    }

    void Update()
    {
        if (isPaused) { return; }

        timePassed += Time.deltaTime;
        UpdateTimerLabel();
    }

    private void UpdateTimerLabel()
    {
        int minutes = (int) timePassed / 60;
        int seconds = (int) timePassed % 60;

        timerLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

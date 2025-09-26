using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeInSeconds = 60f;
    private float time;

    public Animator timesUpAnim;
    public float closingTime = 2f;
    private bool timeIsUp = false;

    public Slider slider;

    private void Start()
    {
        time = timeInSeconds;
        UpdateDisplayTimer();
    }

    void Update()
    {
        if (!timeIsUp)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                time = 0;
                StartCoroutine(WinGame());
            }

            UpdateDisplayTimer();
        }
    }

    private void UpdateDisplayTimer()
    {
        slider.value = time / timeInSeconds;
    }

    IEnumerator WinGame()
    {
        // warning
        timeIsUp = true;
        timesUpAnim.SetTrigger("timesUp");

        // wait
        yield return new WaitForSeconds(closingTime);

        // close out
        ScoreHandler score = FindFirstObjectByType<ScoreHandler>();
        score.SubmitScoreToData();

        SceneChanges sceneChanges = FindFirstObjectByType<SceneChanges>();
        sceneChanges.LoadToSceneByIndex(0);
    }
}

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

    public bool isPaused = true;

    public CameraHandler cameraHandler;

    public AudioSource beepAudio;

    private void Start()
    {
        time = timeInSeconds;
        UpdateDisplayTimer();
    }

    void Update()
    {
        if (isPaused) return;

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
        cameraHandler.SetState(CameraHandler.CameraState.OVERVIEW);

        beepAudio.Play();
        yield return new WaitForSeconds(0.2f);
        beepAudio.Play();

        // wait
        yield return new WaitForSeconds(closingTime - 0.2f);

        // close out
        ScoreHandler score = FindFirstObjectByType<ScoreHandler>();
        score.SubmitScoreToData();

        GetComponent<SceneChanges>().LoadToSceneByIndex(2);
    }
}

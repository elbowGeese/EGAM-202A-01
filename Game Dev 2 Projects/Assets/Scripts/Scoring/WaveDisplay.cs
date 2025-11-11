using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    public TMP_Text textDisplay;
    public TMP_Text unlockText;
    public Animator anim;

    public ParticleSystem fanfareParticle;
    public AudioSource fanfareAudio;

    private void Start()
    {
        HideUnlockText();
    }

    public void DisplayWaveCount(int waveCount, bool lastWave, string unlockedHerb)
    {
        textDisplay.text = "WAVE " + waveCount.ToString();

        if (lastWave)
        {
            textDisplay.text += "\nFINAL WAVE";
        }

        if (unlockedHerb == "") { unlockText.text = ""; }
        else { unlockText.text = "Unlocked <color=blue>" + unlockedHerb + "</color> herb"; }

        anim.SetTrigger("highlight");
    }

    public void PlayFanfare()
    {
        ShowUnlockText();
        fanfareParticle.Play();
        fanfareAudio.Play();
    }

    public void ShowUnlockText()
    {
        unlockText.gameObject.SetActive(true);
    }

    public void HideUnlockText()
    {
        unlockText.gameObject.SetActive(false);
    }
}

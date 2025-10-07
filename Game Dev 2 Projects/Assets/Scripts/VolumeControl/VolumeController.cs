using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public string exposedParam = "Master_Volume";
    public int dataRef = 0;

    void Start()
    {
        audioMixer.SetFloat(exposedParam, Mathf.Log10(VolumeData.GetVolumeData(dataRef)) * 20);
        volumeSlider.value = VolumeData.GetVolumeData(dataRef);
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat(exposedParam, Mathf.Log10(sliderValue) * 20);
        VolumeData.SetVolumeData(dataRef, sliderValue);
    }
}


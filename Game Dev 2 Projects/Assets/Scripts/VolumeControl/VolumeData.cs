using UnityEngine;

public static class VolumeData
{
    private static float masterVolume = 1f;
    private static float musicVolume = 1f;
    private static float ambianceVolume = 1f;
    private static float sfxVolume = 1f;

    public static float GetVolumeData(int num)
    {
        switch (num)
        {
            case 0:
                return masterVolume;
            case 1:
                return musicVolume;
            case 2:
                return ambianceVolume;
            case 3:
                return sfxVolume;
            default:
                return 0f;
        }
    }

    public static void SetVolumeData(int num, float volume)
    {
        switch (num)
        {
            case 0:
                masterVolume = volume;
                break;
            case 1:
                musicVolume = volume;
                break;
            case 2:
                ambianceVolume = volume;
                break;
            case 3:
                sfxVolume = volume;
                break;
            default:
                Debug.Log("Cannot set volume.");
                break;
        }
    }
}


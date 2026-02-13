using UnityEngine;

public class DataLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelData.LoadLevelData();
    }
}

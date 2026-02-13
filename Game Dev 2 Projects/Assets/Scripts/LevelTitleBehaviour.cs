using TMPro;
using UnityEngine;

public class LevelTitleBehaviour : MonoBehaviour
{
    void Start()
    {
        SceneChanges sceneChanges = GetComponent<SceneChanges>();
        TMP_Text levelTitle = GetComponent<TMP_Text>();

        levelTitle.text = "Level " + sceneChanges.GetCurrentSceneIndex();
    }
}

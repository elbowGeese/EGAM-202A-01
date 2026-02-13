using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanges : MonoBehaviour
{
    public void LoadToSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadToSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public int GetCurrentSceneIndex()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;

        return buildIndex;
    }
}

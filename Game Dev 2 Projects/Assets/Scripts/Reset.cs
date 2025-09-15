using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public void ResetCurrentScene()
    {
        // get current scene
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;

        // load current scene
        SceneManager.LoadScene(buildIndex);
    }
}

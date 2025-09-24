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
}

using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static GameObject dontDestroy;

    private void Awake()
    {
        if(dontDestroy != null)
        {
            Destroy(gameObject);
        }

        dontDestroy = gameObject;
        DontDestroyOnLoad(gameObject);
    }
}

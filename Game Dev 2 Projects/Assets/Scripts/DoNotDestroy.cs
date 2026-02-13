using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public static GameObject _instance;

    void Start()
    {
        if(_instance == null)
        {
            _instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

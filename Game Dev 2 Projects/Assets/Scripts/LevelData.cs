using UnityEditor;
using UnityEngine;


[System.Serializable]
public class Level
{
    public bool unlocked;
    public int stars;

    public Level()
    {
        unlocked = false;
        stars = 0;
    }
}

public static class LevelData 
{
    public static Level[] levels = new Level[]
    {
        new Level(),
        new Level(),
        new Level(),
        new Level(),
        new Level(),
        new Level(),
        new Level(),
        new Level(),
        new Level(),
        new Level()
    };

    public static void SaveLevelData()
    {
        string levelsJSON = LevelDataUtility.WrapLevels(levels);
        PlayerPrefs.SetString("levelsJSON", levelsJSON);
        PlayerPrefs.Save();
    }

    public static void LoadLevelData()
    {
        if (PlayerPrefs.HasKey("levelsJSON"))
        {
            Debug.Log("Level Data Found.");
            string levelsJSON = PlayerPrefs.GetString("levelsJSON");
            levels = LevelDataUtility.UnwrapLevels(levelsJSON);
            Debug.Log(levels);
        }
        else
        {
            Debug.Log("No Level Data Found.");
        }
    }
}

public class LevelDataUtility
{
    [System.Serializable]
    public class LevelDataWrapper
    {
        public Level[] wrappedLevels;
    }

    public static Level[] UnwrapLevels(string json)
    {
        LevelDataWrapper wrapper = JsonUtility.FromJson<LevelDataWrapper>(json);
        return wrapper.wrappedLevels;
    }

    public static string WrapLevels(Level[] levels)
    {
        LevelDataWrapper wrapper = new LevelDataWrapper();
        wrapper.wrappedLevels = levels;
        return JsonUtility.ToJson(wrapper);
    }
}

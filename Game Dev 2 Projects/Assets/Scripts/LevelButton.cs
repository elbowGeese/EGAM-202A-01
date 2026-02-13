using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int level;

    public Image[] stars;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if(level == 0) { LevelData.levels[0].unlocked = true; }
        button.interactable = LevelData.levels[level].unlocked;

        for(int i = 0; i < LevelData.levels[level].stars; i++)
        {
            stars[i].color = Color.white;
        }
    }
}

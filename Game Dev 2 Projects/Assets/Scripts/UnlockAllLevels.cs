using UnityEngine;
using UnityEngine.UI;

public class UnlockAllLevels : MonoBehaviour
{
    public Button[] levelButtons;

    public void UnlockAllLevelButtons()
    {
        foreach (Button button in levelButtons)
        {
            button.interactable = true;
        }

        GetComponent<Button>().interactable = false;
    }
}
